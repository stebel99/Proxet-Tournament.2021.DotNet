using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Proxet.Tournament.Initialization
{
    public class Program
    {
        public static async Task Main()
        {
            IConfigurationRoot config = ConfigurationBuilder.Create(Directory.GetCurrentDirectory());

            await AzureServiceBusInitializer.InitializeAsync(config);

            await EntityFrameworkInitializer.InitializeAsync<DealerContext>(
                config,
                "Todd.IO.Dealer.DAL.EF.Migrations",
                SettingKeyConstants.AzureSqlConnectionKey,
                SqlScriptInitializer.InitializeAsync);
        }
    }
}
