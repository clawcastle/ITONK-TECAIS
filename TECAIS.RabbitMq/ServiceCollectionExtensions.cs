using Microsoft.Extensions.DependencyInjection;

namespace TECAIS.RabbitMq
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IEventHandlerManager, EventHandlerManager>();
            return services;
        }
    }
}
