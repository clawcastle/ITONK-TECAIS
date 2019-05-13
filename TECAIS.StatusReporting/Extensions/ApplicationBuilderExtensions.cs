using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMqConnection(this IApplicationBuilder app, Action<StatusReportMessage> handler)
        {
            var rabbitMqConnection = app.ApplicationServices.GetService<IRabbitMqConnection<StatusReportMessage>>();
            var applicationLifeTime = app.ApplicationServices.GetService<IApplicationLifetime>();

            applicationLifeTime.ApplicationStarted.Register(() => OnStarted(rabbitMqConnection, handler));
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(rabbitMqConnection));

            return app;
        }

        private static void OnStarted(IRabbitMqConnection<StatusReportMessage> rabbitMqConnection, Action<StatusReportMessage> handler)
        {
            rabbitMqConnection.RegisterHandler(handler);
        }

        private static void OnStopping(IRabbitMqConnection<StatusReportMessage> rabbitMqConnection)
        {
            rabbitMqConnection.Deregister();
        }
    }
}
