using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.DTO.StockVehiclesUpdate;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.Web;

namespace Airthwholesale.Bal.Logic
{
    public class ICCBatchLogic : IICCBatchLogic
    {

        #region Private properties



        private readonly IRepository<IICCBatchApiDTO> _IICCBatchApiDTORepository;
        private readonly IRepository<JDPVehicleInfoDTO> _jDPVehicleInfoDTORepository;
        private readonly IRepository<JDPExtendedDescriptionsDTO> _jDPExtendedDescriptionsDTORepository;
        private readonly IRepository<JDPListOfPhotosDTO> _jDPListOfPhotosDTORepository;

        private readonly IRepository<JDPVehicleCommentsDTO> _jDPVehicleCommentsDTORepository;
        private readonly IRepository<JDPDealerInfoDTO> _jDPDealerInfoDTORepository;
        private readonly IRepository<JDPPremiumOptionsDTO> _jDPPremiumOptionsDTORepository;
        private readonly IRepository<MapDetailDTO> _mapDetailDTORepository;
        private readonly IRepository<JDPAPICallHistoryDTO> _jDPAPICallHistoryDTORepository;
        private readonly IRepository<JDPSubOptionsDTO> _jDPSubOptionsDTORepository;
        private readonly IRepository<JDPListOfAppliedOffersDTO> _jDPListOfAppliedOffersDTORepository;
        private readonly IRepository<JDPPricingDTO> _jDPPricingDTORepository;
        private readonly IRepository<JDPZStoreValuesDTO> _zStoreValuesDTORepository;
        private readonly IRepository<CBBPricingAPIDetailDTO> _cBBPricingAPIDetailDTORepository;

        private readonly IRepository<JDPCBBPricingDetailForEmailByDealerNamesDTO> _jDPCBBPricingDetailForEmailByDealerNamesDTORepository;  


        public IConfiguration _configuration;
        #endregion

        #region CTOR's
        public ICCBatchLogic(IRepository<IICCBatchApiDTO> IICCBatchApiDTORepository,
            IConfiguration config,
            IRepository<JDPVehicleInfoDTO> jDPVehicleInfoDTORepository,
            IRepository<JDPExtendedDescriptionsDTO> jDPExtendedDescriptionsDTORepository,
            IRepository<JDPListOfPhotosDTO> jDPListOfPhotosDTORepository,
            IRepository<JDPVehicleCommentsDTO> jDPVehicleCommentsDTORepository,
            IRepository<JDPDealerInfoDTO> jDPDealerInfoDTORepository,
            IRepository<JDPPremiumOptionsDTO> jDPPremiumOptionsDTORepository,
            IRepository<MapDetailDTO> mapDetailDTORepository,
            IRepository<JDPAPICallHistoryDTO> jDPAPICallHistoryDTORepository,
            IRepository<JDPSubOptionsDTO> jDPSubOptionsDTORepository,
            IRepository<JDPListOfAppliedOffersDTO> jDPListOfAppliedOffersDTORepository,
            IRepository<JDPPricingDTO> jDPPricingDTORepository,
            IRepository<JDPZStoreValuesDTO> zStoreValuesDTORepository,
            IRepository<CBBPricingAPIDetailDTO> cBBPricingAPIDetailDTORepository,
            IRepository<JDPCBBPricingDetailForEmailByDealerNamesDTO> jDPCBBPricingDetailForEmailByDealerNamesDTORepository




            )
        {
            _IICCBatchApiDTORepository = IICCBatchApiDTORepository;
            _configuration = config;
            _jDPVehicleInfoDTORepository = jDPVehicleInfoDTORepository;
            _jDPExtendedDescriptionsDTORepository = jDPExtendedDescriptionsDTORepository;
            _jDPListOfPhotosDTORepository = jDPListOfPhotosDTORepository;
            _jDPVehicleCommentsDTORepository = jDPVehicleCommentsDTORepository;
            _jDPDealerInfoDTORepository = jDPDealerInfoDTORepository;
            _jDPPremiumOptionsDTORepository = jDPPremiumOptionsDTORepository;
            _mapDetailDTORepository = mapDetailDTORepository;
            _jDPAPICallHistoryDTORepository = jDPAPICallHistoryDTORepository;
            _jDPSubOptionsDTORepository = jDPSubOptionsDTORepository;
            _jDPListOfAppliedOffersDTORepository = jDPListOfAppliedOffersDTORepository;
            _jDPPricingDTORepository = jDPPricingDTORepository;
            _zStoreValuesDTORepository = zStoreValuesDTORepository;
            _cBBPricingAPIDetailDTORepository = cBBPricingAPIDetailDTORepository;
            _jDPCBBPricingDetailForEmailByDealerNamesDTORepository = jDPCBBPricingDetailForEmailByDealerNamesDTORepository;

        }
        #endregion

        #region Interface  Methods

        public async Task<string> GetResponseString(string BaseUri, string api, string format)
        {
            var httpClient = new HttpClient();

            var parameters = new Dictionary<string, string>();
            parameters["ApiKey"] = api;
            parameters["format"] = format;
            var response = await httpClient.PostAsync(BaseUri, new FormUrlEncodedContent(parameters));
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        /// <summary>
        /// For getting GetVehicles list For All Pages response one by one
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<iccApiResponsePageWiseDTO>> GetVehiclesDetailsForAllPages(IICCBatchApiDTO iICCBatchApiDTO)
        {


            // DTO Object for assgin values
            JDPAPIKeyValuesDTO jDPAPIKeyValuesDTO = new JDPAPIKeyValuesDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.JDPower.ToString();
            string KeyCategory = ZStoreKeyCategory.COMMON.ToString();
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.ICCBatchAPIValues.ToString();

            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            // paramters for API call and getting values from Appsetting 
            string APIBaseURL = APIValuesList.APIBaseURL;
            string Apikey = APIValuesList.ApikeyValue;
            string Dealers = APIValuesList.DealersValue;
            string Pageno = APIValuesList.PagenoValue;
            string format = APIValuesList.formatValue;
            var totalpageno = 1;

            List<iccApiResponsePageWiseDTO> ListIccAPiresponse = new List<iccApiResponsePageWiseDTO>();
            for (int i = 1; i <= totalpageno; i++)
            {
                var model = new iccApiResponsePageWiseDTO();
                var APIURL = APIBaseURL;

                // Setting API keys values for API call
                var urlParameters = new Dictionary<string, string>()
                {
                    ["ApiKey"] = Apikey,
                    ["pageno"] = Pageno,
                    ["format"] = format
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
                        var response = JsonConvert.DeserializeObject(documentResponse);

                        // mapped the API Json response to DTO to save in data base.
                        model = JsonConvert.DeserializeObject<iccApiResponsePageWiseDTO>(documentResponse.ToString());

                        totalpageno = model.Paging.PageCount;
                        int pagnoconvert = i + 1;
                        Pageno = pagnoconvert.ToString();
                        ListIccAPiresponse.Add(model);
                    
                    }
                }

            }

            return ListIccAPiresponse;

            // return null;
        }
        /// <summary>
        /// For getting GetVehicles list For All Pages
        /// </summary>
        /// <returns></returns>

        public async Task<JDPStockVehiclesUpdateAPIResponseDTO> GetAllVehicleDetail(IICCBatchApiDTO iICCBatchApiDTO)
        {

            // DTO Object for assgin values
            JDPAPIKeyValuesDTO jDPAPIKeyValuesDTO = new JDPAPIKeyValuesDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.JDPower.ToString();
            string KeyCategory = ZStoreKeyCategory.COMMON.ToString();
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.ICCBatchAPIValues.ToString();

            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            // paramters for API call and getting values from Appsetting 
            string APIBaseURL = APIValuesList.APIBaseURL;
            string Apikey = APIValuesList.ApikeyValue;
            string format = APIValuesList.formatValue;

            // Create a DTO Object for Mapping API response
            var model = new JDPStockVehiclesUpdateAPIResponseDTO();
            var APIURL = APIBaseURL;

            // Setting API keys values for API call
            var urlParameters = new Dictionary<string, string>()
            {
                ["ApiKey"] = Apikey,
                ["format"] = format
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

                    // mapped the API Json response to DTO to save in data base.
                    model = JsonConvert.DeserializeObject<JDPStockVehiclesUpdateAPIResponseDTO>(documentResponse.ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// For getting GetVehicles list Page Wise
        /// </summary>
        /// <returns></returns>
        public async Task<iccApiResponsePageWiseDTO> GetVehiclesDetailsAsync(IICCBatchApiDTO iICCBatchApiDTO)
        {

            // DTO Object for assgin values
            JDPAPIKeyValuesDTO jDPAPIKeyValuesDTO = new JDPAPIKeyValuesDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.JDPower.ToString();
            string KeyCategory = ZStoreKeyCategory.COMMON.ToString();

            // API Other Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.ICCBatchAPIValues.ToString();


            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            
            // Create a DTO Object for Mapping API response
            var model = new iccApiResponsePageWiseDTO();

            // Setting API keys values for API call
            var APIURL = APIValuesList.APIBaseURL;
            var urlParameters = new Dictionary<string, string>()
            {
                ["ApiKey"] = APIValuesList.ApikeyValue,
                ["pageno"] = APIValuesList.PagenoValue,
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

                    // mapped the API Json response to DTO to save in data base.
                    model = JsonConvert.DeserializeObject<iccApiResponsePageWiseDTO>(documentResponse.ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// for Get Vehicles By Dealer Names for Updating Cost ,Special in JDP Pricing table , Vehicle Status  in  JDPVehicleInfo
        /// </summary>
        /// <returns></returns>
        public async Task<JDPStockVehiclesUpdateAPIResponseDTO> GetVehiclesByDealerName(IICCBatchApiDTO iICCBatchApiDTO)
        {

            // DTO Object for assgin values
            JDPAPIKeyValuesDTO jDPAPIKeyValuesDTO = new JDPAPIKeyValuesDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.JDPower.ToString();
            string KeyCategory = ZStoreKeyCategory.COMMON.ToString();

            // API Other Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.ICCBatchAPIValues.ToString();


            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);


            // Create a DTO Object for Mapping API response
            var model = new JDPStockVehiclesUpdateAPIResponseDTO();

            // Setting API keys values for API call
            var APIURL = APIValuesList.APIBaseURL;
            var urlParameters = new Dictionary<string, string>()
            {
                ["ApiKey"] = APIValuesList.ApikeyValue,
                ["dealers"] = iICCBatchApiDTO.Dealers,
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

                    // mapped the API Json response to DTO to save in data base.
                    model = JsonConvert.DeserializeObject<JDPStockVehiclesUpdateAPIResponseDTO>(documentResponse.ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// For getting GetVehiclesCBBAPIByMileageValueList list Page Wise
        /// </summary>
        /// <returns></returns>
        public JDPCBBAPIResponseDTO GetVehiclesCBBAPIByMileageValueList(IICCBatchApiDTO iICCBatchApiDTO)
        {

            var model = new JDPCBBAPIResponseDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.CBB.ToString();
            string KeyCategory = ZStoreKeyCategory.CBBValues.ToString();

            // API Other Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.CBBValues.ToString();


            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);
            // var model = new JDPCBBAPIResponseDTO();

            string sURL = APIValuesList.URL;
            sURL += "/" + HttpUtility.UrlEncode("VIN");
             sURL += "/" + HttpUtility.UrlEncode(iICCBatchApiDTO.vin);
           // sURL += "/" + HttpUtility.UrlEncode("KMHD84LF7JU642352");
           // sURL += "?kilometres" + "=" + HttpUtility.UrlEncode("62196");
            sURL += "?kilometres" + "=" + HttpUtility.UrlEncode(iICCBatchApiDTO.Mileage.ToString());
            sURL += "&country" + "=" + HttpUtility.UrlEncode(APIValuesList.country);
            sURL += "&customerid" + "=" + HttpUtility.UrlEncode(APIValuesList.customerid);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sURL);
            req.Accept = "application/json";
            
            // string sCredentials = "" + APIValuesList.UserName + ":" + APIValuesList.Pwd + "";
            string sCredentials = "AithrAuto_API_WRTL:@!thrAut0#2022!";
            //  string sCredentials = "AithrAuto_API_WRTL:YBWGtWXzGvAZ";
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sCredentials);
            string sEncodedCredentials = Convert.ToBase64String(bytes);
            string sAuthHeader = "Authorization:Basic " + sEncodedCredentials;
            req.Headers.Add(sAuthHeader);

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream());
                JObject doc = JObject.Parse(sr.ReadToEnd());
                model = JsonConvert.DeserializeObject<JDPCBBAPIResponseDTO>(doc.ToString());
            }
            return model;
        }

        /// <summary>
        /// For getting GetVehiclesCBBAPIByMileageValueList list Page Wise
        /// </summary>
        /// <returns></returns>
        public JDPCBBAPIResponseDTO RemoveBackkgroudImage(IICCBatchApiDTO iICCBatchApiDTO)
        {

            var model = new JDPCBBAPIResponseDTO();

            string imageUrl = iICCBatchApiDTO.ImagePath;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var FileName = new String(stringChars);
            FileName = FileName + ".png";
            string FullPath = "UploadedImages//"+ FileName + "";
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Headers.Add("X-Api-Key", "qaBxPiShLotfy9HMyDSkmjUL");
                formData.Add(new StringContent(imageUrl), "image_url");
                formData.Add(new StringContent("auto"), "size");
                var response = client.PostAsync("https://api.remove.bg/v1.0/removebg", formData).Result;

                if (response.IsSuccessStatusCode)
                {
                    FileStream fileStream = new FileStream(FullPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    response.Content.CopyToAsync(fileStream).ContinueWith((copyTask) => { fileStream.Close(); });

                    model.MessagePath = "Image saved in " + fileStream.Name.ToString();
                }
                else
                {
                    Console.WriteLine("Error: " + response.Content.ReadAsStringAsync().Result);
                }
            }
            return model;
        }

        /// <summary>
        /// For getting GetVehicles list Page Wise
        /// </summary>
        /// <returns></returns>
       // public async Task<JDPCBBAPIResponseDTO> GetVehiclesCBBAPIValueList(IICCBatchApiDTO iICCBatchApiDTO)
        public JDPCBBAPIResponseDTO GetVehiclesCBBAPIValueList(IICCBatchApiDTO iICCBatchApiDTO)
        {

            var model = new JDPCBBAPIResponseDTO();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.CBB.ToString();
            string KeyCategory = ZStoreKeyCategory.CBBValues.ToString();

            // API Other Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.CBBValues.ToString();


            // Function for getting API values 
            var APIValuesList = GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);
           // var model = new JDPCBBAPIResponseDTO();

            string sURL = APIValuesList.URL;
            sURL += "/" + HttpUtility.UrlEncode("VIN");
            sURL += "/" + HttpUtility.UrlEncode(iICCBatchApiDTO.vin);
            sURL += "?country" + "=" + HttpUtility.UrlEncode(APIValuesList.country);
            sURL += "&customerid" + "=" + HttpUtility.UrlEncode(APIValuesList.customerid);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sURL);
            req.Accept = "application/json";

            // string sCredentials = "" + APIValuesList.UserName + ":" + APIValuesList.Pwd + "";
            string sCredentials = "AithrAuto_API_WRTL:@!thrAut0#2022!";
            //  string sCredentials = "AithrAuto_API_WRTL:YBWGtWXzGvAZ";
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sCredentials);
            string sEncodedCredentials = Convert.ToBase64String(bytes);
            string sAuthHeader = "Authorization:Basic " + sEncodedCredentials;
            req.Headers.Add(sAuthHeader);

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream());
                JObject doc = JObject.Parse(sr.ReadToEnd());
                model = JsonConvert.DeserializeObject<JDPCBBAPIResponseDTO>(doc.ToString());
            }
            return model;
        }

        #endregion

        #region for getting Lists

        /// <summary>
        /// Get Vehicle Information
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> GetAllVehicleInformation(string DealerId, string Make, int Opcode)
        {
            try
            {
                string procName = SPROC_Names.UspGetAllVehicleInformation.ToString();
                var ParamsArray = new SqlParameter[5];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@DealerId", Value = DealerId, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@Make", Value = Make, DbType = System.Data.DbType.String };
                ParamsArray[4] = new SqlParameter() { ParameterName = "@OpcodeValue", Value = Opcode, DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        public List<JDPVehicleInfoDTO> GetAllVehicleInformationbypage(string DealerId, string Make, int Opcode,int? PageNumber,int? RowsOfPage)
        {
            try
            {
                string procName = SPROC_Names.UspGetAllVehicleInformationByPage.ToString();
                var ParamsArray = new SqlParameter[7];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@DealerId", Value = DealerId, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@Make", Value = Make, DbType = System.Data.DbType.String };
                ParamsArray[4] = new SqlParameter() { ParameterName = "@OpcodeValue", Value = Opcode, DbType = System.Data.DbType.String };

                ParamsArray[5] = new SqlParameter() { ParameterName = "@PageNumber", Value = PageNumber, DbType = System.Data.DbType.String };
                ParamsArray[6] = new SqlParameter() { ParameterName = "@RowsOfPage", Value = RowsOfPage, DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Get Extended Description
        /// </summary>
        /// <returns></returns>
        public List<JDPExtendedDescriptionsDTO> GetVehicleExtendedDescriptionByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetVehicleExtendedDescriptionByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPExtendedDescriptionsDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPExtendedDescriptionsDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPExtendedDescriptionsDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle Photos
        /// </summary>
        /// <returns></returns>
        public List<JDPListOfPhotosDTO> GetVehiclePhotosByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetVehiclePhotosByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPListOfPhotosDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPListOfPhotosDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPListOfPhotosDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle Photos
        /// </summary>
        /// <returns></returns>
        public List<JDPPremiumOptionsDTO> GetVehiclePremiumOptionsByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetVehiclePremiumOptionsByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPPremiumOptionsDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPPremiumOptionsDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPPremiumOptionsDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle Information
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleCommentsDTO> GetVehicleCommentsByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetVehicleCommentsByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleCommentsDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleCommentsDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleCommentsDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle Information
        /// </summary>
        /// <returns></returns>
        public List<JDPDealerInfoDTO> GetDealerInfo()
        {
            try
            {
                string procName = SPROC_Names.UspGetDealerInfo.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPDealerInfoDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle Count By DealerId
        /// </summary>
        /// <returns></returns>
        public List<MapDetailDTO> GetVehicleCountByDealerId()
        {
            try
            {
                string procName = SPROC_Names.UspGetVehicleCountByDealerId.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _mapDetailDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "MapDetailDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<MapDetailDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Dealer List
        /// </summary>
        /// <returns></returns>
        public List<JDPDealerInfoDTO> GetDealerInfoForSearch()
        {
            try
            {
                string procName = SPROC_Names.UspGetDealerInfoForSearch.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPDealerInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Dealer List
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> GetVehicleMakeForSearch()
        {
            try
            {
                string procName = SPROC_Names.UspGetVehicleMakeForSearch.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Dealer List
        /// </summary>
        /// <returns></returns>
        public List<JDPAPICallHistoryDTO> GetJDPAPICallHistory()
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPAPICallHistory.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPAPICallHistoryDTORepository.ExecuteWithJsonResult(procName, "JDPAPICallHistoryDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPAPICallHistoryDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle info by id
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> GetJDPVehicleInfoByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPVehicleInfoByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle info by id
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> UpdateVehicleStatus()
        {
            try
            {
                string procName = SPROC_Names.UspUpdateVehicleStatus.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle sub options
        /// </summary>
        /// <returns></returns>
        public List<JDPSubOptionsDTO> GetVehicleSubOpitonsByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPVehicleSubOpitonsByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPSubOptionsDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPSubOptionsDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPSubOptionsDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get Vehicle list of Applied Offers
        /// </summary>
        /// <returns></returns>
        public List<JDPListOfAppliedOffersDTO> GetVehicleAppliedOffersByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPVehicleAppliedOffersByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPListOfAppliedOffersDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPListOfAppliedOffersDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPListOfAppliedOffersDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle list of Applied Offers
        /// </summary>
        /// <returns></returns>
        public List<JDPPricingDTO> GetVehiclePricingByVehicleIds(string VehicleId)
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPVehiclePricingByVehicleIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VehicleId", Value = VehicleId, DbType = System.Data.DbType.String };
                var resultData = _jDPPricingDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPPricingDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPPricingDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

       

        /// Get ZStore Values
        /// </summary>
        /// <returns></returns>
        public List<JDPZStoreValuesDTO> GetZStoreValues(string StoreName, string KeyCategory)
        {
            try
            {
                string procName = SPROC_Names.UspGetZStoreValues.ToString();
                var ParamsArray = new SqlParameter[4];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@StoreName", Value = StoreName, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@KeyCategory", Value = KeyCategory, DbType = System.Data.DbType.String };
                var resultData = _zStoreValuesDTORepository.ExecuteWithJsonResult(procName, "ZStoreValuesDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPZStoreValuesDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        public async Task<List<JDPDealerInfoDTO>> InsertDealersFromVehicleInfoDealerWise(string OpCode, string DealerId)
        {

            try
            {
                string result = string.Empty;
                string procName = SPROC_Names.UspInsertDealersFromVehicleInfoDealerWise.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = DealerId, DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = OpCode, DbType = System.Data.DbType.String };
                List<JDPDealerInfoDTO> resultData = new List<JDPDealerInfoDTO>();
                resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfo", ParamsArray);
                return resultData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<JDPDealerInfoDTO>> InsertDealersFromVehicleInfo(string OpCode, string DealerId)
        {

            try
            {
                string result = string.Empty;
                string procName = SPROC_Names.UspInsertDealersfromVehicleInfo.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = DealerId, DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = OpCode, DbType = System.Data.DbType.String };
                List<JDPDealerInfoDTO> resultData = new List<JDPDealerInfoDTO>();
                resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfo", ParamsArray);
                return resultData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<JDPDealerInfoDTO>> SyncJDPVehicleInfo(string OpCode, string DealerId)
        {

            try
            {
                string result = string.Empty;
                string procName = SPROC_Names.SP_Synch_Aithr_JDP_Inventory.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@pAsOfDate", Value = DateTime.Now, DbType = System.Data.DbType.String };               
                List<JDPDealerInfoDTO> resultData = new List<JDPDealerInfoDTO>();
                resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfo", ParamsArray);
                return resultData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        ///// Insert Dealers From Vehicle Info
        ///// </summary>
        ///// <returns></returns>
        //public List<JDPDealerInfoDTO> InsertDealersFromVehicleInfo(string OpCode ,string DealerId)
        //{
        //    try
        //    {
        //        string procName = SPROC_Names.UspInsertDealersfromVehicleInfo.ToString();
        //        var ParamsArray = new SqlParameter[2];
        //        ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = DealerId, DbType = System.Data.DbType.String };
        //        ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = OpCode, DbType = System.Data.DbType.String };
        //        var resultData = _jDPDealerInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPDealerInfo", ParamsArray);
        //        return resultData != null ? resultData.ToList() : new List<JDPDealerInfoDTO>();
        //    }
        //    catch (AppException ex)
        //    {
        //        throw;
        //    }
        //}

        /// Insert CBBPricing API Detail
        /// </summary>
        /// <returns></returns>
        public List<CBBPricingAPIDetailDTO> InsertCBBPricingAPIDetail(int PullCount, string APIFuntionName,int VINCountCBBPull)
        {
            try
            {
                string procName = SPROC_Names.UspInsertCBBPricingAPIDetail.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@PullCount", Value = PullCount, DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@APIFuntionName", Value = APIFuntionName, DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@VINCountCBBPull", Value = VINCountCBBPull, DbType = System.Data.DbType.String };
                var resultData = _cBBPricingAPIDetailDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "CBBPricingAPIDetailDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<CBBPricingAPIDetailDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }


        /// <summary>
        /// For UPdate  status in JDPVehicles table
        /// </summary>
        /// <returns></returns>
        public string UpdateVehiclesStatusIndatabase(string xmlapiresponse)
        {
            try
            {
                string procName = SPROC_Names.UspUpdateVehiclesStatusIndatabase.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@xmldata", Value = xmlapiresponse, DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "VehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.FirstOrDefault().updatestatus : "fail";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle info by id
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> GetVINforCBBAPIValues()
        {
            try
            {
                string procName = SPROC_Names.SP_JDP_NewVehicleList.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "VehicleList", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        ///Clean JDP Tables for Older data
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> CleanAllJDPPowerAppTable()
        {
            try
            {
                string procName = SPROC_Names.UspCleanAllJDPPowerAppTable.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        ///Get CBB Pull Detail for Current Month Dealer wise.
        /// </summary>
        /// <returns></returns>
        public List<JDPCBBPricingDetailForEmailByDealerNamesDTO> GetJDPCBBPricingDetailForEmailByDealerNames()
        {
            try
            {
                string procName = SPROC_Names.UspGetJDPCBBPricingDetailForEmailByDealerNames.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPCBBPricingDetailForEmailByDealerNamesDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPCBBPricingDetailForEmailByDealerNamesDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPCBBPricingDetailForEmailByDealerNamesDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vehicle info by id
        /// </summary>
        /// <returns></returns>
        public List<JDPVehicleInfoDTO> CheckJobExecuted()
        {
            try
            {
                string procName = SPROC_Names.UspCheckJobExecuted.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _jDPVehicleInfoDTORepository.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "JDPVehicleInfoDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPVehicleInfoDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }


        /// JDP APIKey Values DTO
        /// </summary>
        /// <returns></returns>
        public JDPAPIKeyValuesDTO GetJDPAPIKeyValues(string StoreName, string KeyCategory, string keyValue)
        {
            try
            {
            
                // DTO Object for assgin values
                JDPAPIKeyValuesDTO jDPAPIKeyValuesDTO = new JDPAPIKeyValuesDTO();
                // Getting Values StoreName and KeyCategory  form ENUM For Common Values
                string KeyCategoryICCBatchValue = keyValue;

                // function for getting CARFAX API Common Values
                if (StoreName == ZStoreName.Carfex.ToString())
                {
                    var commonValues = GetZStoreValues(StoreName, KeyCategory);

                    if (commonValues.Count > 0)
                    {

                        var APIBaseURLList = commonValues.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.APIBaseURL.ToString()).ToList();
                        if (APIBaseURLList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.APIBaseURL = APIBaseURLList[0].KeyValue.ToString();
                        }

                        // For Getting Update from Date Value
                        var ClientIDList = commonValues.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.ClientID.ToString()).ToList();
                        if (ClientIDList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.ClientID = ClientIDList[0].KeyValue.ToString();
                        }

                        // For Getting Update from Date Value
                        var ClientsecretList = commonValues.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.Clientsecret.ToString()).ToList();
                        if (ClientsecretList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.Clientsecret = ClientsecretList[0].KeyValue.ToString();
                        }

                        // For Getting audienceValueList
                        var audienceValueList = commonValues.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.audienceValue.ToString()).ToList();
                        if (audienceValueList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.audienceValue = audienceValueList[0].KeyValue.ToString();
                        }

                        // For Getting granttypeValueList
                        var granttypeValueList = commonValues.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.granttypeValue.ToString()).ToList();
                        if (granttypeValueList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.granttypeValue = granttypeValueList[0].KeyValue.ToString();
                        }
                    }
                }
                // function for getting ICC BATCH API Common Values
                else if (StoreName == ZStoreName.JDPower.ToString())
                {
                    var commonValues = GetZStoreValues(StoreName, KeyCategory);

                    if (commonValues.Count > 0)
                    {
                        // For Getting Base URL
                        var APIBaseURLList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.COMMON.ToString() && c.KeyName == ZStoreNameValue.ApiBaseUrl.ToString()).ToList();
                        if (APIBaseURLList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.APIBaseURL = APIBaseURLList[0].KeyValue.ToString();
                        }

                        // For Getting API Key
                        var APIKeyList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.COMMON.ToString() && c.KeyName == ZStoreNameValue.Apikey.ToString()).ToList();
                        if (APIKeyList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.ApikeyValue = APIKeyList[0].KeyValue.ToString();
                        }


                        // For Getting format
                        var formatList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.COMMON.ToString() && c.KeyName == ZStoreNameValue.Format.ToString()).ToList();

                        if (formatList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.formatValue = formatList[0].KeyValue.ToString();
                        }

                    }

                   
                    // function for getting API ICCBatchAPIValues  Values
                    var APIValuesList = GetZStoreValues(StoreName, KeyCategoryICCBatchValue);

                    if (APIValuesList.Count > 0)
                    {

                        // for getting ApiAddOnUrl

                        var ApiAddOnUrlList = APIValuesList.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.ApiAddOnUrl.ToString()).ToList();
                        if (ApiAddOnUrlList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.ApiAddOnUrl = ApiAddOnUrlList[0].KeyValue.ToString();
                        }

                        // For Getting Page NO
                        var PagenoList = APIValuesList.Where((c) => c.KeyCategory == ZStoreKeyCategory.ICCBatchAPIValues.ToString() && c.KeyName == ZStoreNameValue.Pageno.ToString()).ToList();

                        if (PagenoList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.PagenoValue = PagenoList[0].KeyValue.ToString();
                        }

                        // For Getting Dealer
                        var DealerList = APIValuesList.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.Dealers.ToString()).ToList();
                        if (DealerList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.DealersValue = DealerList[0].KeyValue.ToString();
                        }


                        // For Getting Start Date Value
                        var startDateList = APIValuesList.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.startDate.ToString()).ToList();
                        if (startDateList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.startDateValue = startDateList[0].KeyValue.ToString();
                        }

                        // For Getting Update from Date Value
                        var UpdatesFromList = APIValuesList.Where((c) => c.KeyCategory == KeyCategoryICCBatchValue.ToString() && c.KeyName == ZStoreNameValue.UpdatesFrom.ToString()).ToList();
                        if (UpdatesFromList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.UpdatesFromValue = UpdatesFromList[0].KeyValue.ToString();
                        }

                        // combine both API Base URL and Add On URL
                        string APIURLwithAddOnUrl = jDPAPIKeyValuesDTO.APIBaseURL + jDPAPIKeyValuesDTO.ApiAddOnUrl;
                        jDPAPIKeyValuesDTO.APIBaseURL = APIURLwithAddOnUrl;
                    }
                }
                // function for getting APP SETTING Values
                else if (StoreName == ZStoreName.AppSettings.ToString())
                {
                    var commonValues = GetZStoreValues(StoreName, KeyCategory);
                    if (commonValues.Count > 0)
                    {
                        // For Getting SMTPServerName
                        var SMTPServerNameList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.SMTPServerName.ToString()).ToList();

                        if (SMTPServerNameList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SMTPServerNameValue = SMTPServerNameList[0].KeyValue.ToString();
                        }

                        // For Getting SMTPUserName
                        var SMTPUserNameList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.SMTPUserName.ToString()).ToList();

                        if (SMTPUserNameList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SMTPUserNameValue = SMTPUserNameList[0].KeyValue.ToString();
                        }

                        // For Getting SMTPPassword
                        var SMTPPasswordList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.SMTPPassword.ToString()).ToList();

                        if (SMTPPasswordList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SMTPPasswordValue = SMTPPasswordList[0].KeyValue.ToString();
                        }

                        // For Getting SMTPPort
                        var SMTPPortList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.SMTPPort.ToString()).ToList();

                        if (SMTPPortList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SMTPPortValue = SMTPPortList[0].KeyValue.ToString();
                        }

                        // For Getting EmailAccount
                        var EmailAccountList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.EmailAccount.ToString()).ToList();

                        if (EmailAccountList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.EmailAccountValue = EmailAccountList[0].KeyValue.ToString();
                        }

                        // For Getting EmailAccount
                        var EnableSslList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.SMTP.ToString() && c.KeyName == ZStoreNameValue.EnableSsl.ToString()).ToList();

                        if (EnableSslList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.EnableSslValue = EnableSslList[0].KeyValue.ToString();
                        }

                    }
                }

                // function for getting Jwt SETTING Values
                else if (StoreName == ZStoreName.Jwt.ToString())
                {
                    var commonValues = GetZStoreValues(StoreName, KeyCategory);
                    if (commonValues.Count > 0)
                    {
                        // For Getting Key
                        var KeyList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.Key.ToString()).ToList();

                        if (KeyList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.KeyValue = KeyList[0].KeyValue.ToString();
                        }

                        // For Getting Issuer
                        var IssuerList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.Issuer.ToString()).ToList();

                        if (IssuerList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.IssuerValue = IssuerList[0].KeyValue.ToString();
                        }

                        // For Getting Audience
                        var AudienceList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.Audience.ToString()).ToList();

                        if (AudienceList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.AudienceValue = AudienceList[0].KeyValue.ToString();
                        }

                        // For Getting Subject
                        var SubjectList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.Subject.ToString()).ToList();

                        if (SubjectList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SubjectValue = SubjectList[0].KeyValue.ToString();
                        }

                        // For Getting ValidAudience
                        var ValidAudienceList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.ValidAudience.ToString()).ToList();

                        if (ValidAudienceList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.ValidAudienceValue = ValidAudienceList[0].KeyValue.ToString();
                        }

                        // For Getting ValidIssuer
                        var ValidIssuerList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.ValidIssuer.ToString()).ToList();

                        if (ValidIssuerList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.ValidIssuerValue = ValidIssuerList[0].KeyValue.ToString();
                        }

                        // For Getting ValidIssuer
                        var SecretList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.Secret.ToString()).ToList();

                        if (SecretList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.SecretValue = SecretList[0].KeyValue.ToString();
                        }

                        // For Getting ValidIssuer
                        var JWT_SecretList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.JwtValues.ToString() && c.KeyName == ZStoreNameValue.JWT_Secret.ToString()).ToList();

                        if (JWT_SecretList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.JWT_SecretValue = JWT_SecretList[0].KeyValue.ToString();
                        }

                    }
                }

                // for getting keys for CBB API VALUES
                else if (StoreName == ZStoreName.CBB.ToString())
                {
                    var commonValues = GetZStoreValues(StoreName, KeyCategory);
                    if (commonValues.Count > 0)
                    {
                        // For Getting country for CBB API
                        var countryList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.CBBValues.ToString() && c.KeyName == ZStoreNameValue.country.ToString()).ToList();

                        if (countryList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.country = countryList[0].KeyValue.ToString();
                        }

                        // For Getting country for CBB API
                        var customeridList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.CBBValues.ToString() && c.KeyName == ZStoreNameValue.customerid.ToString()).ToList();

                        if (customeridList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.customerid = customeridList[0].KeyValue.ToString();
                        }


                        // For Getting UserName for CBB API
                        var UserNameList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.CBBValues.ToString() && c.KeyName == ZStoreNameValue.UserName.ToString()).ToList();

                        if (UserNameList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.UserName = UserNameList[0].KeyValue.ToString();
                        }

                        // For Getting Pwd for CBB API
                        var PwdList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.CBBValues.ToString() && c.KeyName == ZStoreNameValue.Pwd.ToString()).ToList();

                        if (PwdList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.Pwd = PwdList[0].KeyValue.ToString();
                        }

                        // For Getting Pwd for CBB API
                        var URLList = commonValues.Where((c) => c.KeyCategory == ZStoreKeyCategory.CBBValues.ToString() && c.KeyName == ZStoreNameValue.URL.ToString()).ToList();

                        if (URLList.Count > 0)
                        {
                            // Assgin Value to DTO column
                            jDPAPIKeyValuesDTO.URL = URLList[0].KeyValue.ToString();
                        }

                    }
                }

                return jDPAPIKeyValuesDTO;
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        #endregion
    }
}
