using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
{
    public class ChargingService : IChargingService
    {
        private readonly HttpClient _httpClient;

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId)
        {
            var chargingInformation = await _httpClient.GetAsync("/charging").ConfigureAwait(false);
            var responseAsString = await chargingInformation.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChargingInformation>(responseAsString);
            return result;
        }
    }
}
