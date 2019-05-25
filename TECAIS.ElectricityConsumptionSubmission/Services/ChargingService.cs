using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TECAIS.ElectricityConsumptionSubmission.Models;

namespace TECAIS.ElectricityConsumptionSubmission.Services
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
            var chargingInformationAsString = await chargingInformationResult.Content.ReadAsStringAsync();
            var chargingInformationDeserialized =
                JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationAsString);
            return chargingInformationDeserialized;
        }
    }
}