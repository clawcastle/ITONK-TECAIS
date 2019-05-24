using System.Threading.Tasks;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
{
    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}