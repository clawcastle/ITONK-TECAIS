using System;
using System.Threading.Tasks;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
{
    public interface IChargingService
    {
        Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId);
    }
}