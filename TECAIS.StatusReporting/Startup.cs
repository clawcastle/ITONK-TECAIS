using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Extensions;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var rabbitHostName = Configuration["RABBIT_HOST_NAME"] ?? "localhost";
            var rabbitRoutingKey = Configuration["RABBIT_ROUTING_KEY"] ?? "status_report";
            services.AddSingleton<IRabbitMqConnection<StatusReportMessage>>(
                new RabbitMqConnection<StatusReportMessage>(rabbitHostName, rabbitRoutingKey));
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

            app.UseRabbitMqConnection(message =>
            {
                Console.WriteLine($"Status report received with status {message.Status.ToString()}");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }


}
