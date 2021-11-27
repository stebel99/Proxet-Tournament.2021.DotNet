using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Proxet.Tournament.DAL.DataContext;
using System.IO;

namespace Proxet.Tournament.DatabaseInitialization
{
    public class ProxetContextFactory : IDesignTimeDbContextFactory<ProxetDbContext>
    {
        ProxetDbContext IDesignTimeDbContextFactory<ProxetDbContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptionsBuilder<ProxetDbContext> builder = new DbContextOptionsBuilder<ProxetDbContext>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ProxetDbContext(builder.Options);
        }
    }
}