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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public PricingService() { }

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            //if default constructor
            using (_httpClient ?? (_httpClient = new HttpClient()))
            {
                try
                {
                    var pricingInformationResult = await _httpClient.GetAsync("https://hourlypricing.comed.com/api?type=currenthouraverage").ConfigureAwait(false);
                    var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                    //JSON string contains an array with an single object - Trimming square brackets before deserializing.
                    var pricingInformationDeserialized =
                        JsonConvert.DeserializeObject<PricingInformation>(pricingInformationAsString
                        .Substring(1, pricingInformationAsString.Length - 3));

                    log.Info("Returning value from API: " + pricingInformationDeserialized.Price);
                    return pricingInformationDeserialized;
                }
                catch(Exception ex)
                {
                    log.Error("Electricity Pricing-API failed with exception: " + ex);
                    throw;
                }

            }
        }
    }
}
