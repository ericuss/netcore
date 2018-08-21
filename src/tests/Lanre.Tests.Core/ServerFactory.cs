
using System.IO;

namespace Lanre.Tests.Core
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Hosting;

    public class ServerFactory
    {
        public static TestServer Server(Action<IServiceCollection> configureServices = null)
        {
            var webHost = new WebHostBuilder()
                .UseStartup<TestApiStartup>()
                ;
            if (configureServices != null)
            {
                webHost = webHost.ConfigureServices(configureServices);
            }
            var integrationTestsPath = Directory.GetCurrentDirectory();
            var applicationPath = Path.GetFullPath(Path.Combine(integrationTestsPath, "../../../../../clients/Lanre.Clients.Api/"));

            webHost.UseContentRoot(applicationPath)
                .UseEnvironment("Development")
                ;


            return new TestServer(webHost)
                ;

        }
    }

}
