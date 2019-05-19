using System;
using System.Threading.Tasks;
using TECAIS.RabbitMq;

namespace TECAIS.ElectricityConsumptionSubmission
{
    public class MeasurementReceivedEventHandler : IEventHandler<Measurement>
    {
        public Task Handle(Measurement @event)
        {
            //Placeholder until we get repositories and so on up and running
            Console.WriteLine($"Received message with id {@event.Id}.");
            return Task.CompletedTask;
        }
    }
}