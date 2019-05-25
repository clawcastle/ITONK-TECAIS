using System;
using System.Threading.Tasks;
using TECAIS.ElectricityConsumptionSubmission.Models;
using TECAIS.ElectricityConsumptionSubmission.Models.Events;
using TECAIS.ElectricityConsumptionSubmission.Services;
using TECAIS.RabbitMq;

namespace TECAIS.ElectricityConsumptionSubmission.Handlers
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
            var getPricingInformationTask = _pricingService.GetPricingInformationAsync(@event.DeviceId);
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
            var price = basePrice * chargingInformation.CurrentTaxRate;
            foreach (var charge in chargingInformation.Charges)
            {
                price += charge;
            }

            return price;
        }
    }
}