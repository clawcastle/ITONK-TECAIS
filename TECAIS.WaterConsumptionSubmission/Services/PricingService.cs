using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json.Linq;
using TECAIS.WaterConsumptionSubmission.Models;

namespace TECAIS.WaterConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient _httpClient;

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            try
            {
                var pricingInformationResult = await _httpClient.GetAsync("series/?api_key=67b6cde351cdb9052134a6221589155b&series_id=TOTAL.KSWHUUS.M").ConfigureAwait(false);
                var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                //get data from JSON Object
                JObject obj = JObject.Parse(pricingInformationAsString);
                var objPrice = (double)obj["series"][0]["data"][0][1];

                PricingInformation pricingInformation = new PricingInformation
                {
                    Price = objPrice
                };

                _log.Info("Water Pricing-API returning value: " + pricingInformation.Price);

                return pricingInformation;
            }
            catch (Exception ex)
            {
                _log.Info("Water Pricing-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
