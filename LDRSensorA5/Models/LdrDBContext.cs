using Microsoft.EntityFrameworkCore;

namespace LDRSensorA5.Models
{
    /// <summary>
    /// <para>
    /// Class <c>LdrDBContext</c> is used to create a dbContext object for the database. It also contains a DbSet property that will create the
    /// table in the database.
    /// </para>
    /// </summary>
    public class LdrDBContext : DbContext
    {
        /// <summary>
        /// LdrDBContext constructor injects the <c>DbContextOptions</c> class
        /// </summary>
        /// <param name="options">It is of type <c>DbContextOptions</c>Sets the options used by DbContext</param>
        public LdrDBContext(DbContextOptions options) : base(options: options)
        {

        }

        /// <summary>
        /// This function is an override of the inbuilt function <c>OnConfiguring</c>. It specifies information about
        /// the configuration of SQLite database, application, and connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
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

        /// <value><c>LDRData</c> is the name of the table in the database. <c>LDRData</c> model type is used to make table.
        /// </value>
        ///  <seealso cref="LDRData"/>
        public DbSet<LDRData>LDRData { get; set; }
    }
}
