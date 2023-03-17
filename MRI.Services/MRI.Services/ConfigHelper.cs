using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MRI.Services
{
    public static class ConfigHelper
    {
        public static string GetConnectionString()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            IConfiguration c = configBuilder.AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            return c.GetRequiredSection("ConnectionStrings").Get<ConnectionStrings>().MRI;
        }
    }
}
