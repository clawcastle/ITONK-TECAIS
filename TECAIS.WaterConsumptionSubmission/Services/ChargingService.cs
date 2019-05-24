using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public class ChargingService : IChargingService
    {
        private readonly HttpClient _httpClient;

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInformation> GetChargingInformationAsync(Guid deviceId)
        {
            var chargingInformationResult = await _httpClient.GetAsync("/charging").ConfigureAwait(false);
            var chargingInformationResultAsString = await chargingInformationResult.Content.ReadAsStringAsync();
            var chargingInformationDeserialized = JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationResultAsString);
            return chargingInformationDeserialized;
        }
    }
}