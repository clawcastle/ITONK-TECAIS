using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
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
                var chargingInformation = await _httpClient.GetAsync("charging/info");
                var responseAsString = await chargingInformation.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChargingInformation>(responseAsString);
                _log.Info("Heat Charging-API return value: " + result.CurrentTaxRate);
                return result;
            }
            catch (Exception ex)
            {
                _log.Error("Heat Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
