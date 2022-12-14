using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RH.UI.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.UI.WEB
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
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                        options.LoginPath = "/Login/Index";

                    });
            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("Admistrator", policy => policy.RequireAssertion(context => context.User.HasClaim(c =>
                c.Value == "admin" && c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")));
                options.AddPolicy("User", policy => policy.RequireAssertion(context => context.User.HasClaim(c =>
              c.Value == "public" && c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")));
                //  (c.Type == "BadgeId" || c.Type == "TemporaryBadgeId")
                // && c.Issuer == "https://microsoftsecurity")));

            });
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseAuthentication();
            // app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home");
               //   The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //    app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //app.Use(async (ctx, next) =>
            //{
            //    await next();

            //    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
            //    {
            //        //Re-execute the request so the user gets the error page
            //       string originalPath = ctx.Request.Path.Value;
            //          ctx.Items["originalPath"] = originalPath;
            //          ctx.Request.Path = "/Home/Index";
            //          await next();
            //        ctx.Response.Redirect("/Home/Index");
            //    }
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallback(context =>
                {
                    // context.User.Claims.ToList()
                   string teste = context.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                    if (teste == "admin")
                        context.Response.Redirect("../Painel");
                    else
                        context.Response.Redirect("../Home");

                    return Task.CompletedTask;
                });
            });



        }
    }
}
