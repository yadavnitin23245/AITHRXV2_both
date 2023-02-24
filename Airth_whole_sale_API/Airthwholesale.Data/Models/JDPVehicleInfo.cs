using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class JDPVehicleInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public int VehicleID { get; set; }

        public int DealerID { get; set; }

        [MaxLength(100)]
        public string DealerName { get; set; }

        public bool? IsNew { get; set; }

        [MaxLength(17)]
        public string VIN { get; set; }

        [MaxLength(100)]
        public string StockNumber { get; set; }

        public bool? IsCertified { get; set; }

        public int? Year { get; set; }

        [MaxLength(100)]
        public string Make { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        [MaxLength(100)]
        public string ModelCode { get; set; }

        [MaxLength(100)]
        public string Trim { get; set; }

        [MaxLength(100)]
        public string BodyName { get; set; }

        [MaxLength(100)]
        public string BodyStyle { get; set; }

        public int? CityMPG { get; set; }

        public int? HwyMPG { get; set; }

        public int? DaysInSotck { get; set; }

        [MaxLength(100)]
        public string ValueSource { get; set; }

        [MaxLength(100)]
        public string ExteriorColor { get; set; }

        [MaxLength(100)]
        public string InteriorColor { get; set; }

        [MaxLength(100)]
        public string InteriorMaterial { get; set; }

        [MaxLength(100)]
        public string Engine { get; set; }

        [MaxLength(100)]
        public string Transmission { get; set; }

        [MaxLength(100)]
        public string TransmissionSpeed { get; set; }


        public DateTime? InStockDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool? IsSpecial { get; set; }

        [MaxLength(100)]
        public string BodyType { get; set; }

        public bool? Locked { get; set; }

        [MaxLength(100)]
        public string VehicleStatus { get; set; }

        public bool? DealerCertified1 { get; set; }

        public bool? DealerCertified2 { get; set; }

        [MaxLength(100)]
        public string GenericExteriorColor { get; set; }

        [MaxLength(200)]
        public string VideoUrl { get; set; }

        [MaxLength(100)]
        public string Drivetrain { get; set; }

        public int? Mileage { get; set; }

        [MaxLength(50)]
        public string Category1 { get; set; }

        [MaxLength(50)]
        public string Category2 { get; set; }

        [MaxLength(50)]
        public string Category3 { get; set; }

        [MaxLength(50)]
        public string Category4 { get; set; }

        [MaxLength(50)]
        public string Category5 { get; set; }

        [MaxLength(100)]
        public string Style { get; set; }

        public int? ChromeStyleID { get; set; }

        [MaxLength(100)]
        public string ZipCode { get; set; }

        [MaxLength(100)]
        public string ExportDealerID { get; set; }

        [MaxLength(100)]
        public string EngineFuelType { get; set; }

        [MaxLength(100)]
        public string ExteriorGenericColorDescription { get; set; }

        [MaxLength(100)]
        public string ExteriorColorCode { get; set; }


        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool? IsInternalSynch { get; set; }

        public DateTime? SynchedDate { get; set; }

        public long? SynchedBy { get; set; }

        public long? InternalID { get; set; }

        public string APICalledBy { get; set; }

        public DateTime? InventoryDate { get; set; }

        public int? DaysInInventory { get; set; }

        
    }
}
