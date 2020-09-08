using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Models;
using WebApplication.Infrastructure;
using WebApplication.Filters;

namespace WebApplication
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            // Labo #12 ---------------------------- Å´
//            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
            // Labo #12 ---------------------------- Å™
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Labo #0 ---------------------------- Å´
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddDbContext<ShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ShopContext")));
            // Labo #0 ---------------------------- Å™

            services.AddRazorPages()
            // Labo #1 ---------------------------- Å´
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Products/Index", "");
                })
            // Labo #1 ---------------------------- Å™
            // Labo #11 ---------------------------- Å´
                .AddMvcOptions(options =>
                 {
                     options.Filters.Add<LoggingPageFilter>();
                 });
            // Labo #11 ---------------------------- Å™
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
