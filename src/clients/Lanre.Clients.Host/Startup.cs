namespace Lanre.Clients.Host
{
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Api;
    using Infrastructure.Entities.Configuration;

    public class Startup
    {
        private readonly IHostingEnvironment _currentEnvironment;
        private readonly Settings _appSettings;

        public Startup(IHostingEnvironment env)
        {
            this._currentEnvironment = env;

            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                configBuilder.AddUserSecrets<Startup>(optional: true);
            }

            var builder = configBuilder.Build();
            this._appSettings = builder.Get<Settings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServicesApi()
                    .AddCustomSwagger()
                    ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app
                .UseCustomErrorHandler()
                .ConfigureApi()
                .UseCustomSwagger()
                //.Run(async (context) =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //})
                ;
        }
    }
}
