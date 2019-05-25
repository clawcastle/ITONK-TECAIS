using System;
using System.Threading.Tasks;
using TECAIS.ElectricityConsumptionSubmission.Models;

namespace TECAIS.ElectricityConsumptionSubmission.Services
{
    public interface IChargingService
    {
        Task<ChargingInformation> GetChargingInformationAsync(Guid deviceId);
    }
}