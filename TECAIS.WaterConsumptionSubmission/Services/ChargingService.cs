using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public class ChargingService : IChargingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public ChargingService() { }

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId)
        {
            try
            {
                var chargingInformationResult = await _httpClient.GetAsync("charging/info").ConfigureAwait(false);
                var chargingInformationResultAsString = await chargingInformationResult.Content.ReadAsStringAsync();
                var chargingInformationDeserialized = JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationResultAsString);

                _log.Info("Water Charging-API return value: " + chargingInformationDeserialized.CurrentTaxRate);
                return chargingInformationDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Water Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}