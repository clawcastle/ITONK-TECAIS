using System;
using System.Threading.Tasks;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public interface IChargingService
    {
        Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId);
    }
}