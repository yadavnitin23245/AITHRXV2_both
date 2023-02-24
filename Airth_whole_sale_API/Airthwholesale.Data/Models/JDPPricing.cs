using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class JDPPricing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public long? JDPVehicleInfoId { get; set; }

        public int? VehicleID { get; set; }

        public int? DealerID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtraPrice1 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtraPrice2 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtraPrice3 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? List { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Special { get; set; }

        public int? Order { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }
    }
}
