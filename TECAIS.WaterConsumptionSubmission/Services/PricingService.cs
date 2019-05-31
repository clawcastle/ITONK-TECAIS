using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PricingService));
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
                    var pricingInformationResult = await _httpClient.GetAsync("http://api.eia.gov/series/?api_key=67b6cde351cdb9052134a6221589155b&series_id=TOTAL.KSWHUUS.M").ConfigureAwait(false);
                    var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                    //get data from JSON Object
                    JObject obj = JObject.Parse(pricingInformationAsString);
                    var objPrice = (double)obj["series"][0]["data"][0][1];

                    PricingInformation pricingInformation = new PricingInformation();

                    pricingInformation.Price = objPrice;

                    return pricingInformation;
                }
                catch (Exception ex)
                {
                    log.Info("Water API failed with exception: " + ex);
                    throw;
                }
            }
        }
    }
}
