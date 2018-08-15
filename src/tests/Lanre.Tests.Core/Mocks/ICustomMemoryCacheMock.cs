namespace Lanre.Tests.Core.Mocks
{
    using Microsoft.Extensions.Caching.Memory;
    using Infrastructure.Cache;
    using Moq;

    public class ICustomMemoryCacheMock
    {
        public static Mock<ICustomMemoryCache> Mocked
        {
            get
            {
                var memoryCache = Mock.Of<ICustomMemoryCache>();
                var cachEntry = Mock.Of<ICacheEntry>();

                var cache = Mock.Get(memoryCache);
                cache
                    .Setup(m => m.CreateEntry(It.IsAny<object>()))
                    .Returns(cachEntry);
                return cache;
            }
        }
    }

}
