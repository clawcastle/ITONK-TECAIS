using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private readonly HttpClient _httpClient;

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            var chargingInformation = await _httpClient.GetAsync("/price").ConfigureAwait(false);
            var responseAsString = await chargingInformation.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PricingInformation>(responseAsString);
            return result;
        }
    }

    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}
