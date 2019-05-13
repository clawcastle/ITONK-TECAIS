using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;

namespace SampleConsumer
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
            var rabbitHostName = Configuration["RABBITMQ_HOST_NAME"] ?? "localhost";
            var rabbitRoutingKey = Configuration["RABBIT_ROUTING_KEY"] ?? "heat";
            services.AddSingleton<IRabbitMqConnection<Measurement>>(
                new RabbitMqConnection<Measurement>(rabbitHostName, rabbitRoutingKey));
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

            app.UseRabbitMqConnection(measurement =>
            {
                Measurements.MeasurementsList.Add(measurement);
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
