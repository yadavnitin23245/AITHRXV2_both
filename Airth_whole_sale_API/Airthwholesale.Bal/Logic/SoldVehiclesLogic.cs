using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace Airthwholesale.Bal.Logic
{
    public class SoldVehiclesLogic : ISoldVehiclesLogic
    {

        #region Private properties
        public IConfiguration _configuration;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }

        private readonly IRepository<JDPSoldVehicleListDTO> _JDPSoldVehicleListDTO;


        #endregion

        #region CTOR's
        public SoldVehiclesLogic(
            IConfiguration config, IICCBatchLogic iICCBatchLogicBAL, IRepository<JDPSoldVehicleListDTO> JDPSoldVehicleListDTO)
        {

            _configuration = config;
            _IICCBatchLogicBAL = iICCBatchLogicBAL;
                _JDPSoldVehicleListDTO = JDPSoldVehicleListDTO; 
        }
        #endregion

        #region Interface  Methods

        /// <summary>
        /// For getting Get Sold Vehicles list By Date 
        /// </summary>
        /// <returns></returns>
        public async Task<JDPSoldVehicleListDTO> GetSoldVehiclesByDate(JDPSoldVehiclesParametersDTO soldVehiclesParametersDTO)
        {

            try
            {

                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string StoreName = ZStoreName.JDPower.ToString();
                string KeyCategory = ZStoreKeyCategory.COMMON.ToString();
                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string KeyCategoryICCBatchValues = ZStoreKeyCategory.StockVehiclesAPIValues.ToString();

                // Function for getting API values 
                var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

                // Create a DTO Object for Mapping API response
                var model = new JDPSoldVehicleListDTO();

                // Setting API keys values for API call
                var APIURL = APIValuesList.APIBaseURL;


                // for getting Current Date for passing to API
                DateTime date = DateTime.Now;
                string formattedDate = date.ToString("yyyy-MM-dd");

                // Setting API keys values for API call
                var urlParameters = new Dictionary<string, string>()
                {
                    ["ApiKey"] = APIValuesList.ApikeyValue,
                    ["startDate"] = APIValuesList.startDateValue,
                    ["format"] = APIValuesList.formatValue
                };

                // Creating a complete API URL with parameters
                var apiUrl = QueryHelpers.AddQueryString(APIURL, urlParameters);

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


                        model = JsonConvert.DeserializeObject<JDPSoldVehicleListDTO>(documentResponse.ToString());
                    }
                }
                return model;

            }
            catch (AppException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// For Update Price in JDP Price table
        /// </summary>
        /// <returns></returns>
        /// 
        public string UpdateSoldpricebyAPIIndatabase(string xmlapiresponse)
        {
            try
            {
                string procName = SPROC_Names.UspUpdatetBulksoldvehicleUsingXML.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@xmldata", Value = xmlapiresponse, DbType = System.Data.DbType.String };            
                var resultData = _JDPSoldVehicleListDTO.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "soldvehicle", ParamsArray);
                return resultData != null ? resultData.FirstOrDefault().status :"fail";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
