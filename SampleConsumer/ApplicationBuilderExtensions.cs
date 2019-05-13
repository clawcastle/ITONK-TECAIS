using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;

namespace SampleConsumer
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMqConnection(this IApplicationBuilder app, Action<Measurement> handler)
        {
            var rabbitMqConnection = app.ApplicationServices.GetService<IRabbitMqConnection<Measurement>>();
            var applicationLifeTime = app.ApplicationServices.GetService<IApplicationLifetime>();

            applicationLifeTime.ApplicationStarted.Register(() => OnStarted(rabbitMqConnection, handler));
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(rabbitMqConnection));

            return app;
        }

        private static void OnStarted(IRabbitMqConnection<Measurement> rabbitMqConnection, Action<Measurement> handler)
        {
            rabbitMqConnection.RegisterHandler(handler);
        }

        private static void OnStopping(IRabbitMqConnection<Measurement> rabbitMqConnection)
        {
            rabbitMqConnection.Deregister();
        }
    }
}