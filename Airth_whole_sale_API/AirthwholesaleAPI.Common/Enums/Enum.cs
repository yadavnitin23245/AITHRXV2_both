using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AirthwholesaleAPI.Common.Enums
{
    // Common ENUM for using in any where in project

    public enum ZStoreName
    {
        Carfex,
        JDPower,
        AppSettings,
        Jwt,
        CBB
    }

    public enum ZStoreKeyCategory
    {
        CarfexAPIValues,
        COMMON,
        ICCBatchAPIValues,
        StockVehiclesAPIValues,
        stockvehiclesupdatesApiValues,
        SMTP,
        ConnectionStrings,
        JwtValues,
        CBBValues
    }

    public enum ZStoreNameValue
    {
        // ENUM for Common Value for ICC batch API
        ApiBaseUrl,
        Apikey,
        Format,

        // ENUM for other Values  ICC batch API
        ApiAddOnUrl,
        Dealers,
        Pageno,
        startDate,
        UpdatesFrom,

        // ENUM Key for Carfex values
        APIBaseURL,
        ClientID,
        Clientsecret,
        audienceValue,
        granttypeValue,

        // ENUM for Keys of AppSetting

        SMTPServerName,
        SMTPUserName,
        SMTPPassword,
        SMTPPort,
        EmailAccount,
        EnableSsl,
        ResetpasswordURL,

        // Values for JWT Key
        Key,
        Issuer,
        Audience,
        Subject,
        ValidAudience,
        ValidIssuer,
        Secret,
        JWT_Secret,

        // Values for CBB API
        country,
        customerid,
        UserName,
        Pwd,
        URL


    }

    public enum APICallFunctions
    {
        GetAllVehiclesInformationAllPages,
        GetVehiclesByDealerNames

    }

    public enum EmailSend
    {
        [Display(Description = "peter@aithr.ca")]
        Admin = 1,

        [Display(Description = "pankaj.sadiyal@spadezgroup.com")]
        User = 2,
    }

  

    public enum SPROC_Names
    {
        UspGetAllUsers,
        UspGetCountryList,
        UspGetStateListByCountryId,
        UspGetCityListByCountryId,
        UspGetUserDetailById,
        UspGetAllVehicleInformation,
        UspGetAllVehicleInformationByPage,
        UspGetVehicleCommentsByVehicleIds,
        UspGetVehiclePhotosByVehicleIds,
        UspGetVehicleExtendedDescriptionByVehicleIds,
        UspGetDealerDetails,
        UspGetVehicleGetPricingInfoByVehicleIds,
        UspGetVehiclePremiumOptionsByVehicleIds,
        UspGetVehiclePremiumSubOptionsByVehicleIds,
        UspGetVehicleListOfAvailableOfferByVehicleIds,
        UspGetDealerInfo,
        UspGetVehicleCountByDealerId,
        UspGetDealerInfoForSearch,
        UspGetVehicleMakeForSearch,
        UspGetJDPAPICallHistory,
        UspGetJDPVehicleInfoByVehicleIds,
        UspGetJDPVehicleSubOpitonsByVehicleIds,
        UspGetJDPVehicleAppliedOffersByVehicleIds,
        UspGetJDPVehiclePricingByVehicleIds,
        UspGetZStoreValues,
        UspUpdatetBulksoldvehicleUsingXML,
        UspInsertDealersfromVehicleInfo,
        SP_Synch_Aithr_JDP_Inventory,
        UspInsertDealersFromVehicleInfoDealerWise,
        UspUpdateVehicleStatus,
        UspUpdateVehiclesStatusIndatabase,
        SP_JDP_NewVehicleList,
        UspInsertCBBPricingAPIDetail,
        UspCleanAllJDPPowerAppTable,
        UspGetJDPCBBPricingDetailForEmailByDealerNames,
        UspCheckJobExecuted,
        UspGetRemoveTextDashboard,
        UspGetCleanImagesDetails,
        UspGet_UncleanImagefor_clean,
        UspGetAllGroups,
        UspGetGroupById,
        UspGetRolesById,
        UspGetAllRoles,
        UspGetUsersById,
        UspGetAllUsersDetails,
        UspGetAllDealers,
        UspGetDealersByIds,
        UspGetAllSubscription,
        UspGetPositionsByIds,
        UspGetAllPositions

    }
}
