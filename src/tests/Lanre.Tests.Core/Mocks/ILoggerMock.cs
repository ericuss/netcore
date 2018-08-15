namespace Lanre.Tests.Core.Mocks
{
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ILoggerMock

    {
        public static Mock<ILogger<TClass>> Mocked<TClass>()
        {
            var logger = new Mock<ILogger<TClass>>();
            return logger;
        }
    }
}
