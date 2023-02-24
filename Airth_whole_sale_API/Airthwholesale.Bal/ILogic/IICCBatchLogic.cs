using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.DTO.StockVehiclesUpdate;
using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface IICCBatchLogic
    {
        /// <summary>
        /// For getting GetVehicles list Page Wise
        /// </summary>
        /// <returns></returns>
        Task<iccApiResponsePageWiseDTO> GetVehiclesDetailsAsync(IICCBatchApiDTO iICCBatchApiDTO);

        /// <summary>
        /// For getting GetVehicles list For All Pages response one by one
        /// </summary>
        /// <returns></returns>
        Task<List<iccApiResponsePageWiseDTO>> GetVehiclesDetailsForAllPages(IICCBatchApiDTO iICCBatchApiDTO);
        List<JDPVehicleInfoDTO> GetAllVehicleInformationbypage(string DealerId, string Make, int Opcode, int? PageNumber, int? RowsOfPage);


        /// <summary>
        /// For getting GetVehicles list For All Pages
        /// </summary>
        /// <returns></returns>
        Task<JDPStockVehiclesUpdateAPIResponseDTO> GetAllVehicleDetail(IICCBatchApiDTO iICCBatchApiDTO);


        /// <summary>
        /// Get Vehicle Information
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> GetAllVehicleInformation(string DealerId, string Make, int Opcode);

        /// <summary>
        /// Get Vehicle Photos
        /// </summary>
        /// <returns></returns>
        List<JDPExtendedDescriptionsDTO> GetVehicleExtendedDescriptionByVehicleIds(string VehicleId);


        /// <summary>
        /// Get Vehicle Photos
        /// </summary>
        /// <returns></returns>
        List<JDPListOfPhotosDTO> GetVehiclePhotosByVehicleIds(string VehicleId);


        /// <summary>
        /// Get Vehicle Comments
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleCommentsDTO> GetVehicleCommentsByVehicleIds(string VehicleId);

        /// <summary>
        /// Get JDPPremiumOptions
        /// </summary>
        /// <returns></returns>
        List<JDPPremiumOptionsDTO> GetVehiclePremiumOptionsByVehicleIds(string VehicleId);


        /// <summary>
        /// Get Dealer info
        /// </summary>
        /// <returns></returns>
        List<JDPDealerInfoDTO> GetDealerInfo();


        /// <summary>
        /// Get Vehicle Count By DealerId
        /// </summary>
        /// <returns></returns>
        List<MapDetailDTO> GetVehicleCountByDealerId();

        /// <summary>
        /// GetDealer Info For Search
        /// </summary>
        /// <returns></returns>
        List<JDPDealerInfoDTO> GetDealerInfoForSearch();

        /// <summary>
        /// GetVehicleMakeForSearch
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> GetVehicleMakeForSearch();

        /// <summary>
        /// GetJDPAPICallHistory
        /// </summary>
        /// <returns></returns>
        List<JDPAPICallHistoryDTO> GetJDPAPICallHistory();


        /// <summary>
        /// Get Vehicle Photos
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> GetJDPVehicleInfoByVehicleIds(string VehicleId);

        /// <summary>
        /// Get Vehicle Sub options
        /// </summary>
        /// <returns></returns>
        List<JDPSubOptionsDTO> GetVehicleSubOpitonsByVehicleIds(string VehicleId);

        /// Get Vehicle Sub options
        /// </summary>
        /// <returns></returns>
        List<JDPListOfAppliedOffersDTO> GetVehicleAppliedOffersByVehicleIds(string VehicleId);


        /// Get Vehicle Pricing info
        /// </summary>
        /// <returns></returns>
        List<JDPPricingDTO> GetVehiclePricingByVehicleIds(string VehicleId);


        /// Get ZStore Values
        /// </summary>
        /// <returns></returns>
        List<JDPZStoreValuesDTO> GetZStoreValues(string StoreName , string KeyCategory);


        /// Get ZStore Values
        /// </summary>
        /// <returns></returns>
        JDPAPIKeyValuesDTO GetJDPAPIKeyValues(string StoreName, string KeyCategory, string keyValue);

        /// Insert Dealers From Vehicle Info
        /// </summary>
        /// <returns></returns>

       


        /// UPdate  Vehicle status
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> UpdateVehicleStatus();


        /// <summary>
        /// For getting CBB Values
        /// </summary>
        /// <returns></returns>

        JDPCBBAPIResponseDTO GetVehiclesCBBAPIValueList(IICCBatchApiDTO iICCBatchApiDTO);

        /// <summary>
        /// Get Vehicles By Dealer Names for Updating Cost ,Special in JDP Pricing table , Vehicle Status  in  JDPVehicleInfo
        /// </summary>
        /// <returns></returns>
        Task<JDPStockVehiclesUpdateAPIResponseDTO> GetVehiclesByDealerName(IICCBatchApiDTO iICCBatchApiDTO);

        /// <summary>
        /// For UPdate  status in JDPVehicles table
        /// </summary>
        /// <returns></returns>
        string UpdateVehiclesStatusIndatabase(string xmlapiresponse);


        /// <summary>
        /// For Getting VIN NUMBERS FOR CBB API
        /// </summary>
        /// <returns></returns>

        List<JDPVehicleInfoDTO> GetVINforCBBAPIValues();

        /// <summary>
        /// For Getting CBB Pricing API Detail
        /// </summary>
        /// <returns></returns>
        List<CBBPricingAPIDetailDTO> InsertCBBPricingAPIDetail(int PullCount, string APIFuntionName, int VINCountCBBPull);

        /// <summary>
        /// For Clean All JDPPower App Table
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> CleanAllJDPPowerAppTable();


        /// <summary>
        /// For Getting CBB Pricing Details for send in email
        /// </summary>
        /// <returns></returns>
        List<JDPCBBPricingDetailForEmailByDealerNamesDTO> GetJDPCBBPricingDetailForEmailByDealerNames();


        /// UPdate  Vehicle status
        /// </summary>
        /// <returns></returns>
        List<JDPVehicleInfoDTO> CheckJobExecuted();

        /// <summary>
        /// For getting CBB Values
        /// </summary>
        /// <returns></returns>

        JDPCBBAPIResponseDTO GetVehiclesCBBAPIByMileageValueList(IICCBatchApiDTO iICCBatchApiDTO);


        Task<List<JDPDealerInfoDTO>> InsertDealersFromVehicleInfoDealerWise(string OpCode, string DealerId);


        Task<List<JDPDealerInfoDTO>> InsertDealersFromVehicleInfo(string OpCode, string DealerId);

        Task<List<JDPDealerInfoDTO>> SyncJDPVehicleInfo(string OpCode, string DealerId);


        JDPCBBAPIResponseDTO RemoveBackkgroudImage(IICCBatchApiDTO iICCBatchApiDTO);

    }
}
