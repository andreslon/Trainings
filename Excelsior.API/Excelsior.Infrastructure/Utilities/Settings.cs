using Excelsior.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Excelsior.Infrastructure.Utilities
{
    public class Settings : ISettings
    {
        public string GetSetting(string key)
        {
            return GetValue(key, "AppSettings");
        }

        public string GetConnectionString(string key)
        {
            return GetValue(key, "ConnectionStrings");
        }

        private string GetValue(string key, string file)
        {
            string pathRoot = Path.Combine(Directory.GetCurrentDirectory(), "Configurations");

            var builder = new ConfigurationBuilder()
                              .SetBasePath(pathRoot)
                             .AddJsonFile($"{file}.json");
            var configuration = builder.Build();
            var value = configuration[$"{key}"];
            var decrypted = Security.Decrypt(value);
            return decrypted;
        }
         
    }
}
