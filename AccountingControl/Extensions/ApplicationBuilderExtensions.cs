using AccountingControl.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TECAIS.AccountingControl.Models.Events;
using TECAIS.RabbitMq;

namespace TECAIS.AccountingControl.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            var applicationLifeTime = app.ApplicationServices.GetService<IApplicationLifetime>();
            eventBus.Subscribe<AccountingMessage, AccountMessageReceivedHandler>("accounting");
            applicationLifeTime.ApplicationStopping.Register(() => OnStopping(eventBus));
            return app;
        }

        private static void OnStopping(IEventBus eventBus)
        {
            eventBus.Deregister();
        }
    }
}