using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class iccApiResponsePageWiseDTO
    {
        public List<Datum> Data { get; set; }
        public Paging Paging { get; set; }
        public Error Error { get; set; }
        public DateTime ResponseDate { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public VehicleInfo VehicleInfo { get; set; }

        public List<ListOfPhoto> ListOfPhotos { get; set; }
        public List<ListOfOption> ListOfOption { get; set; }
        public Pricing Pricing { get; set; }
        public List<ListOfAppliedOffers> ListOfAppliedOffers { get; set; }
        public List<PremiumOption> PremiumOptions { get; set; }
        public List<ListOfAvailableOffer> ListOfAvailableOffers { get; set; }
        public List<ListOfShowOffer> ListOfShowOffer { get; set; }
    }

    public class ListOfAppliedOffers
    {
        public string Price { get; set; }
        public string Amount { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public DateTime OfferStartDate { get; set; }

        public DateTime OfferEndDate { get; set; }
    }

    public class PremiumOption
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<SubOption> SubOptions { get; set; }
        public double Msrp { get; set; }
    }

    public class SubOption
    {
        public int? OptionalOptionID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Msrp { get; set; }
        public double Invoice { get; set; }
        public bool IsChecked { get; set; }
        public int? packageParentID { get; set; }
        public int? packageID { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class ListOfShowOffer
    {
        public string Name { get; set; }
        public string Target { get; set; }
        //  public ProgramValue programValue { get; set; }
    }

    public class ExtendedDescriptions
    {
        public string ExtendedVehicleName { get; set; }
        public string ExtendedBodyStyle { get; set; }
        public string ExtendedDrivetrain { get; set; }
        public string ExtendedEngineType { get; set; }
        public string ExtendedDisplacement { get; set; }
        public string ExtendedFuelSystem { get; set; }
        public string ExtendedTransmissionDescripCont { get; set; }
        public string ExtendedTransType { get; set; }
        public string ExtendedBestmakename { get; set; }
        public string ExtendedBestmodelname { get; set; }
        public string ExtendedBeststylename { get; set; }
        public string ExtendedBesttrimname { get; set; }
    }

    public class ListOfPhoto
    {
        public string VehiclePhotoID { get; set; }
        public string PhotoUrl { get; set; }
        public int Order { get; set; }
        public DateTime PhotoTimestamp { get; set; }
    }

    public class ListOfAvailableOffer
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public string OwnershipStatus { get; set; }
        public string Description { get; set; }
        public string EndDate { get; set; }
        public string Price { get; set; }
        public ProgramValue programValue { get; set; }
        public int DealerID { get; set; }
        public string StackabilityList { get; set; }
        public int IncentiveID { get; set; }
        public string ProgrammerNumber { get; set; }
        public string Priority { get; set; }
    }

    public class ProgramValue
    {
        public List<ValueVariationList> valueVariationList { get; set; }
    }

    public class ValueVariationList
    {
        public string tierStart { get; set; }
        public string tierEnd { get; set; }
        public string tiers { get; set; }
        public List<ProgramValueList> programValueList { get; set; }
    }

    public class ProgramValueList
    {
        public string description { get; set; }
        public List<TermList> termList { get; set; }
        public string cash { get; set; }
    }

    public class TermList
    {
        public int to { get; set; }
        public int from { get; set; }
        public string valueType { get; set; }
        public string financialDisclosure { get; set; }
        public double value { get; set; }
    }


    public class ListOfOption
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }

        public int Msrp { get; set; }

        public string ImageUrl { get; set; }
        public string Order { get; set; }
    }

    public class Paging
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalRecordCount { get; set; }
    }

    public class Pricing
    {
        public int Cost { get; set; }
        public int List { get; set; }
        public int Special { get; set; }
        public int ExtraPrice1 { get; set; }
        public int ExtraPrice2 { get; set; }
        public int ExtraPrice3 { get; set; }
    }


    public class VehicleInfo
    {
        public int VehicleID { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public bool IsNew { get; set; }
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public bool IsCertified { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelCode { get; set; }
        public string Trim { get; set; }
        public string BodyName { get; set; }
        public string BodyStyle { get; set; }
        public int CityMPG { get; set; }
        public int HwyMPG { get; set; }
        public int DaysInSotck { get; set; }
        public string ValueSource { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string InteriorMaterial { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string TransmissionSpeed { get; set; }
        public string Comments { get; set; }
        public string Comments2 { get; set; }
        public string Comments3 { get; set; }
        public string Comments4 { get; set; }
        public string Comments5 { get; set; }
        public DateTime InStockDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public object IsSpecial { get; set; }
        public string BodyType { get; set; }
        public bool Locked { get; set; }
        public string VehicleStatus { get; set; }
        public bool DealerCertified1 { get; set; }
        public bool DealerCertified2 { get; set; }
        public string GenericExteriorColor { get; set; }
        public string VideoUrl { get; set; }
        public string Drivetrain { get; set; }
        public int Mileage { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        public string Category5 { get; set; }
        public ExtendedDescriptions ExtendedDescriptions { get; set; }
        public string Style { get; set; }
        public int ChromeStyleID { get; set; }
        public string ZipCode { get; set; }
        public string ExportDealerID { get; set; }
        public string EngineFuelType { get; set; }
        public string ExteriorGenericColorDescription { get; set; }
        public string ExteriorColorCode { get; set; }
        public string OneOwner { get; set; }
        public string VehicleHistoryReportLink { get; set; }
        public string InteriorColorCode { get; set; }
        public string DoorCount { get; set; }
        public string EngineDisplacement { get; set; }
    }




}
