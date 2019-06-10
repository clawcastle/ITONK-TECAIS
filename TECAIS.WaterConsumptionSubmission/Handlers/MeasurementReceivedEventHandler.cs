using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.WaterConsumptionSubmission.Models;
using TECAIS.WaterConsumptionSubmission.Models.Events;
using TECAIS.WaterConsumptionSubmission.Services;

namespace TECAIS.WaterConsumptionSubmission.Handlers
{
    public class MeasurementReceivedEventHandler : IEventHandler<Measurement>
    {
        private readonly IChargingService _chargingService;
        private readonly IPricingService _pricingService;
        private readonly IEventBus _eventBus;

        public MeasurementReceivedEventHandler(IChargingService chargingService, IPricingService pricingService, IEventBus eventBus)
        {
            _chargingService = chargingService;
            _pricingService = pricingService;
            _eventBus = eventBus;
        }

        public async Task Handle(Measurement @event)
        {
            var chargingInformationTask = _chargingService.GetChargingInformationForConsumerAsync(@event.DeviceId);
            var pricingInformationTask = _pricingService.GetPricingInformationAsync();
            await Task.WhenAll(chargingInformationTask, pricingInformationTask).ConfigureAwait(false);

            var chargingInformation = await chargingInformationTask.ConfigureAwait(false);
            var pricingInformation = await pricingInformationTask.ConfigureAwait(false);
            var price = CalculatePrice(pricingInformation.Price, chargingInformation);

            var accountingMessage = AccountingMessage.Create(price, @event.HouseId, pricingInformation, chargingInformation);
            _eventBus.Publish(accountingMessage);
        }

        private double CalculatePrice(double basePrice, ChargingInformation chargingInformation)
        {
            var price = basePrice * chargingInformation.CurrentTaxRate;
            foreach (var charge in chargingInformation.Charges)
            {
                price += charge;
            }

            return price;
        }
    }
}