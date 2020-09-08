using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication.Infrastructure;
using WebApplication.Services;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Labo #5 ---------------------------- Å´
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            // Labo #5 ---------------------------- Å™
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                                IConfiguration config/*Labo #4*/, ILogger<Startup> logger/*Labo #7*/)
        {
            if (env.IsDevelopment())
            {
                // Labo #7 ---------------------------- Å´
                logger.LogInformation("In Development environment.");
                // Labo #7 ---------------------------- Å™
                app.UseDeveloperExceptionPage();
            }
            // Labo #8 ---------------------------- Å´
            else
            {
                app.UseExceptionHandler(error =>
                {
                    error.Run(async context =>
                    {
                        await context.Response.WriteAsync("Custom Error Message for Production.");
                    });
                });
            }
            // Labo #8 ---------------------------- Å™

            // Labo #3 ---------------------------- Å´
            app.UseStaticFiles();
            // Labo #3 ---------------------------- Å™

            app.UseRouting();

            // Labo #5 ---------------------------- Å´
            app.Map("/product", productApp =>
            {
                productApp.Run(async (context) =>
                {
                    var svc = app.ApplicationServices.GetService<IProductService>();
                    var products = svc.Get();
                    foreach (var product in products)
                    {
                        await context.Response.WriteAsync(product.Name + "\r\n");
                    }
                });
            });
            // Labo #5 ---------------------------- Å™

            // Labo #4 ---------------------------- Å´
            app.Map("/config", configApp =>
            {
                configApp.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Environment = " + env.EnvironmentName + "\r\n");
                    var logLevel = config["Logging:LogLevel:Default"];
                    await context.Response.WriteAsync("LogLevel = " + logLevel + "\r\n");
                    var connection = config.GetConnectionString("DefaultConnection");
                    await context.Response.WriteAsync("ConnectionStrings = " + connection + "\r\n");
                });
            });
            // Labo #4 ---------------------------- Å™

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
