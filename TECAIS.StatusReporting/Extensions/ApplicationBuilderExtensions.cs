using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Data;
using TECAIS.StatusReporting.Handlers;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            var applicationLifeTime = app.ApplicationServices.GetService<IApplicationLifetime>();
            eventBus.Subscribe<StatusReportMessage, StatusMessageReceivedHandler>("status");
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(eventBus));
            return app;
        }

        private static void OnStopping(IEventBus eventBus)
        {
            eventBus.Deregister();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            var databaseHostName = Environment.GetEnvironmentVariable("STATUS_SQLSERVER_SERVICE_HOST");
            var databasePassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
            var connectionString =
                $"Data Source = {databaseHostName}; User Id = SA; Password = {databasePassword}; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            services.AddDbContext<StatusDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
