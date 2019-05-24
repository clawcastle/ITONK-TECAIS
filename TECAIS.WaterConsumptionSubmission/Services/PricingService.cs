using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private readonly HttpClient _httpClient;

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PricingInformation> GetPricingInformationAsync(Guid deviceId)
        {
            var pricingInformation = await _httpClient.GetAsync("/price").ConfigureAwait(false);
            var responseAsString = await pricingInformation.Content.ReadAsStringAsync();
            var pricingInformationDeserialized = JsonConvert.DeserializeObject<PricingInformation>(responseAsString);
            return pricingInformationDeserialized;
        }
    }
}
