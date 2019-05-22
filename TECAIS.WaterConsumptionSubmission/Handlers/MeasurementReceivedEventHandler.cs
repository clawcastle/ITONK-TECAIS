using System;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.WaterConsumptionSubmission.Models.Events;

namespace TECAIS.WaterConsumptionSubmission.Handlers
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