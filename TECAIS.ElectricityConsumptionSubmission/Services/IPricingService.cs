using System;
using System.Threading.Tasks;
using TECAIS.ElectricityConsumptionSubmission.Models;

namespace TECAIS.ElectricityConsumptionSubmission.Services
{
    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}