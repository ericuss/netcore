namespace Lanre.Clients.Api.Tests
{
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Hosting;

    public class ServerFactory
    {
        public static TestServer Server => new TestServer(new WebHostBuilder().UseStartup<TestApiStartup>());
    }

}
