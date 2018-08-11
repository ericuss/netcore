using Lanre.Clients.Host.Middelwares;

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;

    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
