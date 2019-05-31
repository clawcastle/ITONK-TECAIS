using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using TECAIS.ElectricityConsumptionSubmission.Models;

namespace TECAIS.ElectricityConsumptionSubmission.Services
{
    public class ChargingService : IChargingService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient _httpClient;

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInformation> GetChargingInformationAsync(Guid deviceId)
        {

            try
            {
                var chargingInformationResult = await _httpClient.GetAsync("/charging").ConfigureAwait(false);
                var chargingInformationAsString = await chargingInformationResult.Content.ReadAsStringAsync();
                var chargingInformationDeserialized =
                    JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationAsString);

                log.Info("Electricity Charging-API return value: " + chargingInformationDeserialized.CurrentTaxRate);

                return chargingInformationDeserialized;
            }
            catch(Exception ex)
            {
                log.Error("Electricity Charging-API failed with exception: " + ex);
                throw;
            }




        }
    }
}