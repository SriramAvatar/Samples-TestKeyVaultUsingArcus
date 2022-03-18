using Arcus.Security.Core.Caching.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;


namespace TestKeyvault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureSecretStore((config, option) =>
                {
                    //Keyvault URL
                    var keyVaultUrl = config["AppSettings_KeyVault_Url"];
                    //A value in minutes for the cache retention
                    var keyVaultCacheExpiration = config["AppSettings_KeyVault_CacheExpiration"];
                    var cacheConfiguration = new CacheConfiguration(TimeSpan.FromMinutes(double.Parse(keyVaultCacheExpiration)));

                    option.AddAzureKeyVaultWithManagedIdentity(keyVaultUrl, cacheConfiguration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.UseStartup<Startup>();
                });
        }
    }
}
