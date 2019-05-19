using System.Threading.Tasks;

namespace TECAIS.RabbitMq
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}