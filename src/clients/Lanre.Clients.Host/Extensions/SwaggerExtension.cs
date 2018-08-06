
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Swashbuckle.AspNetCore.Swagger;
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.DescribeAllParametersInCamelCase();
                setup.DescribeStringEnumsInCamelCase();
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "Lanre Api",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanre");
                      });
        }
    }
}
