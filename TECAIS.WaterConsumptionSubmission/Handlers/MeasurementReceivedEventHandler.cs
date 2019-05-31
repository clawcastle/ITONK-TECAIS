using System;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.WaterConsumptionSubmission.Models;
using TECAIS.WaterConsumptionSubmission.Models.Events;
using TECAIS.WaterConsumptionSubmission.Services;

namespace TECAIS.WaterConsumptionSubmission.Handlers
{
    public class MeasurementReceivedEventHandler : IEventHandler<Measurement>
    {
        private readonly IPricingService _pricingService;
        private readonly IChargingService _chargingService;
        private readonly IEventBus _eventBus;

        public MeasurementReceivedEventHandler(IPricingService pricingService, IChargingService chargingService, IEventBus eventBus)
        {
            _pricingService = pricingService;
            _chargingService = chargingService;
            _eventBus = eventBus;
        }

        public async Task Handle(Measurement @event)
        {
            var getPricingInformationTask = _pricingService.GetPricingInformationAsync();
            var getChargingInformationTask = _chargingService.GetChargingInformationAsync(@event.DeviceId);
            await Task.WhenAll(getPricingInformationTask, getChargingInformationTask).ConfigureAwait(false);

            var pricingInformation = await getPricingInformationTask;
            var chargingInformation = await getChargingInformationTask;
            var price = CalculatePrice(pricingInformation.Price, chargingInformation);
            var accountingMessage = AccountingMessage.Create(price, pricingInformation, chargingInformation);
            _eventBus.Publish(accountingMessage);
        }

        private double CalculatePrice(double basePrice, ChargingInformation chargingInformation)
        {
            return basePrice * chargingInformation.CurrentTaxRate + chargingInformation.Charges.Sum();
        }
    }
}