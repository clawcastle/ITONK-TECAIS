using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;
using TECAIS.WaterConsumptionSubmission.Extensions;
using TECAIS.WaterConsumptionSubmission.Handlers;
using TECAIS.WaterConsumptionSubmission.Services;

namespace TECAIS.WaterConsumptionSubmission
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
            services.AddEventBus();
            services.AddTransient<MeasurementReceivedEventHandler>();
            services.AddTransient<IChargingService, ChargingService>();
            services.AddTransient<IPricingService, PricingService>();
            services.AddHttpClient<IChargingService, ChargingService>(sp =>
            {
                var chargingServiceHostName =
                    Environment.GetEnvironmentVariable("CHARGING_LOADBALANCER_SERVICE_HOST") ?? "localhost:44350";
                var chargingServiceBaseUrl = $"https://{chargingServiceHostName}/api/";
                sp.BaseAddress = new Uri(chargingServiceBaseUrl);
            });
            services.AddHttpClient<IPricingService, PricingService>(sp =>
            {
                sp.BaseAddress = new Uri("http://api.eia.gov/");
            });
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

            app.ConfigureEventBus();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
