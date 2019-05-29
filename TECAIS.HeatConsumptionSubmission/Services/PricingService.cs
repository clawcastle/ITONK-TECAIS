using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TECAIS.HeatConsumptionSubmission.Models;

namespace TECAIS.HeatConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private HttpClient _httpClient;

        public PricingService()
        {
        }

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            //if default constructor
            using (_httpClient ?? (_httpClient = new HttpClient()))
            {
                var pricingInformationResult = await _httpClient.GetAsync("http://api.eia.gov/series/?api_key=67b6cde351cdb9052134a6221589155b&series_id=NG.N9130US3.A").ConfigureAwait(false);
                var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                //get data from JSON Object
                JObject obj = JObject.Parse(pricingInformationAsString);
                var objPrice = (double)obj["series"][0]["data"][0][1];

                PricingInformation pricingInformation = new PricingInformation();
                pricingInformation.Price = objPrice;

                return pricingInformation;
            }
        }
    }
}
