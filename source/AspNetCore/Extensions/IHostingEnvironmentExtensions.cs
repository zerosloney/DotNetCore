using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DotNetCore.AspNetCore
{
    public static class IHostingEnvironmentExtensions
    {
        public static IConfiguration Configuration(this IHostingEnvironment environment)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json")
                .AddJsonFile($"AppSettings.{environment.EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
