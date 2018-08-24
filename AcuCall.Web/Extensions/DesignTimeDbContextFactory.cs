using AcuCall.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AcuCall.Web
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AcuCallsContext>
    {
        public AcuCallsContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AcuCallsContext>();
            var connectionString = configuration.GetConnectionString("AcuCallContext");
            builder.UseSqlServer(connectionString);
            return new AcuCallsContext(builder.Options);
        }
    }
}
