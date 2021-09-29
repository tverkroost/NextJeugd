using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NEXTjeugd.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class NEXTjeugdDbContextFactory : IDesignTimeDbContextFactory<NEXTjeugdDbContext>
    {
        public NEXTjeugdDbContext CreateDbContext(string[] args)
        {
            NEXTjeugdEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<NEXTjeugdDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new NEXTjeugdDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NEXTjeugd.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
