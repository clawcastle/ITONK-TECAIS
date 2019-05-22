using System;
using System.Threading.Tasks;
using TECAIS.HeatConsumptionSubmission.Models.Events;
using TECAIS.RabbitMq;

namespace TECAIS.HeatConsumptionSubmission.Handlers
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