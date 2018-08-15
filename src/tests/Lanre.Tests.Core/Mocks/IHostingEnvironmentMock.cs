
namespace Lanre.Tests.Core.Mocks
{
    using Microsoft.AspNetCore.Hosting;
    using Moq;

    public class IHostingEnvironmentMock

    {
        public static Mock<IHostingEnvironment> Mocked
        {
            get
            {
                var env = new Mock<IHostingEnvironment>();
                return env;
            }
        }
    }
}
