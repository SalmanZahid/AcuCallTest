using AcuCall.Core.Interfaces;
using AcuCall.Core.Services;
using AcuCall.Infrastructure.Data;
using AcuCall.Infrastructure.Data.Interfaces;
using AcuCall.Web.AutoMapper;
using AcuCall.Web.Extensions;
using AcuCall.Web.Hubs;
using AcuCall.Web.Subscriptions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AcuCall.Web
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                       {
                           options.LoginPath = "/Account/Login/";
                       });



            services.AddMvc();

            // SETUP DB CONTEXT
            services.AddDbContext<AcuCallsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AcuCallContext")));
            services.AddTransient<IAcuCallsContext, AcuCallsContext>();

            // REGISTER SERVICES
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserSessionService, UserSessionService>();
            services.AddTransient<IReportService, ReportService>();

            // REGISTER REPOSITORIESz 
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserSessionRepository, UserSessionRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();

            services.AddSingleton<UserSubscription, UserSubscription>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ObjectProfile>();
                cfg.AddProfile<ModelProfile>();
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSignalR(routes =>
            {
                routes.MapHub<UserHub>("/user");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSqlTableDependency<UserSubscription>(Configuration.GetConnectionString("AcuCallContext"));

        }
    }
}
