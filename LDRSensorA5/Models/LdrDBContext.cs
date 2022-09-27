using Microsoft.EntityFrameworkCore;

namespace LDRSensorA5.Models
{
    public class LdrDBContext : DbContext
    {
        public LdrDBContext(DbContextOptions options) : base(options: options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ConStr");
                optionsBuilder.UseSqlite(connectionString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                optionsBuilder.EnableSensitiveDataLogging();

            }
        }
    }
}
