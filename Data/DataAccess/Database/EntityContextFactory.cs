using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Database
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<EntityContext>
    {
        EntityContext IDesignTimeDbContextFactory<EntityContext>.CreateDbContext(string[] args)
        {
            // TODO: This is a short hack to get the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"/Users/armendu/Documents/win-it/Presentation/Presentation/appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<EntityContext>();
            var connectionString = configuration.GetConnectionString("WinItConnectionString");
            
            builder.UseMySql(connectionString);

            return new EntityContext(builder.Options);
        }
    }
}