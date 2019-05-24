using System;
using System.Threading.Tasks;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync(Guid deviceId);
    }
}