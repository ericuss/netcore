
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
            var webHost = new WebHostBuilder().UseStartup<TestApiStartup>();
            if (configureServices != null) webHost = webHost.ConfigureServices(configureServices);
            return new TestServer(webHost);

        }
    }

}
