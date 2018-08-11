using Microsoft.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;

    public static class ApiVersionExtension
    {
        public static IServiceCollection AddCustomApiVersion(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = ApiVersionReader.Combine(
                                            new QueryStringApiVersionReader("api-version"),
                                            new HeaderApiVersionReader("api-version")
                                        );
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

        }

        //public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        //{
        //    return app;
        //}
    }
}
