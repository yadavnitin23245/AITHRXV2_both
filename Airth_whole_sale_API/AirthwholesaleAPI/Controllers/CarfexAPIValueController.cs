using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using AirthwholesaleAPI.Common.Enums;
//using GraphQL;
//using GraphQL.Client.Http;
//using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AirthwholesaleAPI.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarfexAPIValueController : ControllerBase
    {
        // Inject IConfiguration for access the Appsetting values
        public IConfiguration _configuration;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        public CarfexAPIValueController(IConfiguration config, IICCBatchLogic iICCBatchLogicBAL)
        {
            _configuration = config;
            _IICCBatchLogicBAL = iICCBatchLogicBAL;
        }

        /// <summary>
        /// For getting CarfaxApi Token By Client Id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("GetCarfaxApiTokenByClientId")]
        public async Task<IActionResult> SearchAsync([FromBody] CarfaxValuesDTO carfaxValuesDTO)
        {

            try
            {

                // We have masked the Client Id and Client Secret in fornt end so getting the valves from App setting

                string StoreName = ZStoreName.Carfex.ToString();
                string KeyCategory = ZStoreKeyCategory.CarfexAPIValues.ToString();
                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string KeyCategoryICCBatchValues = ZStoreKeyCategory.CarfexAPIValues.ToString();

                // Function for getting API values 
                var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

                string ClientID = "";
                string Clientsecret = "";
                // We have masked the Client Id and Client Secret in fornt end so getting the valves from App setting
                if (carfaxValuesDTO.clientid.Contains("*"))
                {
                    ClientID = APIValuesList.ClientID;
                }
                else
                {
                    ClientID = carfaxValuesDTO.clientid;
                }

                if (carfaxValuesDTO.clientsecret.Contains("*"))
                {
                    Clientsecret = APIValuesList.Clientsecret;
                }
                else
                {
                    Clientsecret = carfaxValuesDTO.clientsecret;
                }



                //Creating HttpClient for API call
                using (var client = new HttpClient())
                {
                    // Setting values in HttpClient for API call
                    client.BaseAddress = new Uri(carfaxValuesDTO.APIBaseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // set values for parameters for API call
                    var parametersValues = new
                    {
                        client_id = ClientID,
                        client_secret = Clientsecret,
                        audience = carfaxValuesDTO.audienceValue,
                        grant_type = carfaxValuesDTO.granttypeValue,
                    };

                    // Getting response of API.
                    HttpResponseMessage apiResponse = await client.PostAsJsonAsync(client.BaseAddress, parametersValues);

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        // Getting content of response
                        var documentResponse = await apiResponse.Content.ReadAsStringAsync();

                        // mapped the response with DTO
                        var model = JsonConvert.DeserializeObject<CarfaxAPIResponseDTO>(documentResponse.ToString());

                        return Ok(model);
                    }
                }

                return Ok("");
            }
            catch (AppException ex)
            {
                return null;
            }
        }


        /// <summary>
        /// For getting CarfaxApi Token by Default Values from Appsettings
        /// </summary>
        /// <returns></returns>
        /// 


        //[HttpGet("GetGraphQL")]
        //public async Task<IActionResult> GetGraphQL()
        //{

        //    try
        //    {
        //        var graphQLHttpClientOptions = new GraphQLHttpClientOptions
        //    {
        //        EndPoint = new Uri("https://inventory-rds-stg.cd-dev.ca/graphql")
        //    };
        //    var httpClient = new HttpClient();      
        //    httpClient.DefaultRequestHeaders.Add("x-api-key", "da2-xxqdj74ibzhttgdca3u5niytim");
         
        //        var graphQLClient = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer(), httpClient);

        //        var graphRequest = new GraphQLRequest
        //        {
        //            Query = @"
        //            { getVehicles {
        //              stock_number
        //                vin
        //            }
        //            }
        //            "
        //        };

        //        var graphQLResponse = await graphQLClient.SendQueryAsync<GetVehicles>(graphRequest);
        //          return Ok(graphQLResponse);
        //    }
        //    catch (AppException ex)
        //    {
        //        return null;
        //    }
        //}

        [HttpGet("GetCarfaxApiToken")]
        public async Task<IActionResult> SearchAsync()
        {
            try
            {

                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string StoreName = ZStoreName.Carfex.ToString();
                string KeyCategory = ZStoreKeyCategory.CarfexAPIValues.ToString();
                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string KeyCategoryICCBatchValues = ZStoreKeyCategory.CarfexAPIValues.ToString();

                // Function for getting API values 
                var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);


                // Creating HTTP client for API call
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIValuesList.APIBaseURL);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Set parameters values in URL
                    var parametersValues = new
                    {
                        client_id = APIValuesList.ClientID,
                        client_secret = APIValuesList.Clientsecret,
                        audience = APIValuesList.audienceValue,
                        grant_type = APIValuesList.granttypeValue,
                    };

                    // Getting API Resposne
                    HttpResponseMessage apiResponse = await client.PostAsJsonAsync(client.BaseAddress, parametersValues);

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        // read content from API resposne
                        var documentResponse = await apiResponse.Content.ReadAsStringAsync();

                        // mapped the resposne with DTO
                        var model = JsonConvert.DeserializeObject<CarfaxAPIResponseDTO>(documentResponse.ToString());

                        return Ok(model);
                    }
                }

                return Ok("");
            }
            catch (AppException ex)
            {
                return null;
            }
        }

    }



    public class GraphQLMovieResponse
    {

       // public GetVehicles GetVehicles { get; set; }

    };

    public class GetVehicles
    {

        public string stock_number { get; set; }
        public string vin { get; set; }

    }
}
