using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.ElectricityConsumptionSubmission.Models;

namespace TECAIS.ElectricityConsumptionSubmission.Services
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
            var pricingInformationResult = await _httpClient.GetAsync("/price").ConfigureAwait(false);
            var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();
            var pricingInformationDeserialized =
                JsonConvert.DeserializeObject<PricingInformation>(pricingInformationAsString);
            return pricingInformationDeserialized;
        }
    }
}
