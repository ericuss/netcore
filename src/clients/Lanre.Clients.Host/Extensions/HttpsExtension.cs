

using Lanre.Infrastructure.Entities.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;

    public static class HttpsExtension
    {
        public static IServiceCollection AddCustomHttps(this IServiceCollection services, Settings settings)
        {
            services
                .AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    options.HttpsPort = settings.HttpsConfig.Port;
                })
                ;
            return services;
        }

        public static IApplicationBuilder UseCustomHttps(this IApplicationBuilder app, IHostingEnvironment env)
        {
            return app
                    .AddIf(!env.IsDevelopment(), x => x.UseHsts())
                    .UseHttpsRedirection()
                ;
        }
    }
}
