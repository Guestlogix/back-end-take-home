using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RouteSearch.API.Midlewares;
using RouteSearch.Application.EntityMapper;
using RouteSearch.Application.RouteFinder;
using RouteSearch.Application.RouteFinder.Interfaces;
using RouteSearch.Domain.Repositories;
using RouteSearch.Domain.Services.Interfaces;
using RouteSearch.Domain.Services.RouteFinder;
using RouteSearch.Infrastructure.Data;
using RouteSearch.Infrastructure.Repositories;

namespace RouteSearch.API
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
            var airportsPath = Configuration.GetValue<string>("airportsFilePath");
            var airlinesPath = Configuration.GetValue<string>("airlinesFilePath");
            var routesPath = Configuration.GetValue<string>("routesFilePath");

            var filePaths = new Dictionary<string, string>();
            filePaths.Add("airport", airportsPath);
            filePaths.Add("airline", airlinesPath);
            filePaths.Add("route", routesPath);

            services.AddSingleton<DataContext>(m => {
                var dataLoading = new DataLoading(filePaths);
                return dataLoading.GetDataContext();
            });

            services.AddScoped<IMapper>(m => MapperConfig.Configure());
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IRouteFinder, RouteFinder>();
            services.AddScoped<IRouteFinderService, RouteFinderService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<RouteSearchExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
