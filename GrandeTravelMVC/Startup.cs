using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GrandeTravelMVC.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GrandeTravelMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddScoped<IDataService<CustomerProfile>, DataService<CustomerProfile>>();
            services.AddScoped<IDataService<ProviderProfile>, DataService<ProviderProfile>>();
            services.AddScoped<IDataService<Location>, DataService<Location>>();
            services.AddScoped<IDataService<Package>, DataService<Package>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddScoped<IDataService<Feedback>, DataService<Feedback>>();
            

            services.AddIdentity<IdentityUser, IdentityRole>
                (
                   config =>
                   {
                       config.Password.RequireDigit = false;
                       config.Password.RequiredLength = 3;
                       config.Password.RequireNonAlphanumeric = false;
                       config.Password.RequireUppercase = false;
                   }
                ).AddEntityFrameworkStores<MyDbContext>();

            services.AddDbContext<MyDbContext>();

            services.ConfigureApplicationCookie(options => { options.AccessDeniedPath = "/Account/Denied"; });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "{authorisationserveraddress}";
                    options.Audience = "{audience}";
                });     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvcWithDefaultRoute();

            //SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
