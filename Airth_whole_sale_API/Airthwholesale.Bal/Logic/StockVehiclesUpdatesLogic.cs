using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.DTO.StockVehiclesUpdate;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace Airthwholesale.Bal.Logic
{
    public class StockVehiclesUpdatesLogic : IStockVehiclesUpdatesLogic
    {

        #region Private properties
        public IConfiguration _configuration;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        #endregion

        #region CTOR's
        public StockVehiclesUpdatesLogic(IConfiguration config, IICCBatchLogic iICCBatchLogicBAL)
        {
            _configuration = config;
            _IICCBatchLogicBAL = iICCBatchLogicBAL;
        }
        #endregion

        #region Interface  Methods

        /// <summary>
        /// For getting Get Stock Vehicles Updates list
        /// </summary>
        /// <returns></returns>

        public async Task<JDPStockVehiclesUpdateAPIResponseDTO> GetStockVehiclesUpdates(JDPSoldVehiclesParametersDTO soldVehiclesParametersDTO)
        {

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.JDPower.ToString();
            string KeyCategory = ZStoreKeyCategory.COMMON.ToString();
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.stockvehiclesupdatesApiValues.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

          
            // Create a DTO Object for Mapping API response
            var model = new JDPStockVehiclesUpdateAPIResponseDTO();

            var APIURL = APIValuesList.APIBaseURL;

            // for getting Current Date for passing to API
            DateTime date = DateTime.Now;
            string UpdatesFromDate = date.ToString("yyyy-MM-dd");

            // Setting API keys values for API call
            var urlParameters = new Dictionary<string, string>()
            {
                ["ApiKey"] = APIValuesList.ApikeyValue,
                ["UpdatesFrom"] = APIValuesList.UpdatesFromValue,
                ["format"] = APIValuesList.formatValue
            };

            // Creating a complete API URL with parameters
            var apiUrl = QueryHelpers.AddQueryString(APIURL, urlParameters);
            try
            {
                // Creating HttpClient for all API
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // getting response of API
                    HttpResponseMessage apiResponse = await client.GetAsync(apiUrl);
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        // Getting content from API response 
                        var documentResponse = await apiResponse.Content.ReadAsStringAsync();

                        // mapped the API Json response to DTO to save in data base.
                        model = JsonConvert.DeserializeObject<JDPStockVehiclesUpdateAPIResponseDTO>(documentResponse.ToString());
                    }
                }
            }
            catch (AppException ex)
            {

            }
            return model;
        }

        #endregion

    }
}
