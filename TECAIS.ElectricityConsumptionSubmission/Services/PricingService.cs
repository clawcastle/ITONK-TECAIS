using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using TECAIS.ElectricityConsumptionSubmission.Models;



namespace TECAIS.ElectricityConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {   
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;
        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            try
            {
                var pricingInformationResult = await _httpClient.GetAsync("api?type=currenthouraverage").ConfigureAwait(false);
                var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                //JSON string contains an array with an single object - Trimming square brackets before deserializing.
                var pricingInformationDeserialized =
                    JsonConvert.DeserializeObject<PricingInformation>(pricingInformationAsString
                    .Substring(1, pricingInformationAsString.Length - 3));

                _log.Info("Electricity Pricing-API returning value: " + pricingInformationDeserialized.Price);
                return pricingInformationDeserialized;
            }
            catch(Exception ex)
            {
                _log.Error("Electricity Pricing-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
