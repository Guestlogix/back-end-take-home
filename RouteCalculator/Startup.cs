using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RouteCalculator.Contracts;
using RouteCalculator.Repositories;
using RouteCalculator.Services;
using RouteCalculator.Settings;
using Swashbuckle.AspNetCore.Swagger;

[assembly: InternalsVisibleTo("RouteCalculator.RouteCalculatorTest")]

namespace RouteCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<RepositorySettings>(Configuration.GetSection("RepositorySettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Route Calculator", Version = "v1" });
            });

            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IRouteCalculatorRepository, RouteCalculatorRepository>();
            services.AddSingleton<IRouteCalculatorService, RouterCalculatorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Route Calculator");
            });
        }
    }
}
