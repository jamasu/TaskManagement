﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManagement.Repository;
using TaskManagerDbDAL;
using TaskManagment.Model;
using TaskManagment.Repository;

namespace TaskManagment
{
    public class Startup
    {

        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("configuration.json");
            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<TaskSettings>(_configuration.GetSection("TaskDB"));
            services.AddTransient<TaskManagerDbContext>();
            services.AddTransient<TaskRepository<TaskHandler>>();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute("CreateNewTask", "{controller=Home}/{action=CreateNewTask}");
                routes.MapRoute("Default route", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
