using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleHourglass.Commonality.Base;
using LittleHourglass.Commonality.BaseExpand;
using LittleHourglass.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LittleHourglass.WebRouter
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
            services.AddControllers();
            //ϵͳ���������ע��
            services.SupervisorConfiguration();
            //ҵ����������ע��
            //services.Configuration();
            //��λ����
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            //Enable Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            //�������Ȳ���

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    //defaults:new { controller = "Home", action = "MainPepiline"}
                );

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            //app.ApplicationBuilder();
            await SchedulersQuartz._().Initialize();
            LoggerHelper.Info("LittleHourglas start!!!");
        }
    }
}
