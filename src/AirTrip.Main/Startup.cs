using System;
using System.IO;
using System.Reflection;
using AirTrip.Services.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AirTrip.Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            services.AddSwaggerGen(ConfigureSwagger);

            services.AddDataProviders();
            services.AddCustomServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(ConfigureSwaggerEndpoint);
        }

        private static void ConfigureSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
            {
                Title = "Guest Logix Search API",
                Contact = new Contact
                {
                    Name = "Vivian Ajay Monis"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        private static void ConfigureSwaggerEndpoint(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Guest Logix Search API");
        }
    }
}