using LDRSensorA5.Models;
using LDRSensorA5.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Text.Json;

namespace LDRSensorA5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ICommunicationService,CommunicationService>();
            builder.Services.AddScoped<ILdrService,LdrService>();
            builder.Services.AddScoped<IManualModeService, ManualModeService>();
           


            builder.Services.AddDbContext<LdrDBContext>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration)
              .Enrich.FromLogContext()
              .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog(logger);

            var app = builder.Build();

            //---added this scoped code----
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LdrDBContext>();
                //use context
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

           

            app.UseCors("corsapp");

                

            app.UseSerilogRequestLogging();

            //creating the json file
            string fileName = "configuration.json";
            if(!File.Exists(fileName))
            {
                ConfigurationData configData = new ConfigurationData();
                configData.DefaultLowerThreshold = 100;
                configData.DefaultUpperThreshold = 200;
                configData.UpperThreshold = 200;
                configData.LowerThreshold = 100;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(configData, options);
                File.WriteAllText(fileName, jsonString);
            }
            

            app.Run();
        }
    }
}