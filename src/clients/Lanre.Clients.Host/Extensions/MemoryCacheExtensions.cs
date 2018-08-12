
using Lanre.Infrastructure.Cache;

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;

    public static class MemoryCacheExtensions
    {
        public static IServiceCollection AddCustomCache(this IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddSingleton<ICustomMemoryCache, CustomMemoryCache>()
                ;

            return services;
        }

    }
}
