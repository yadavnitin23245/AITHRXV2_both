using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class JDPExtendedDescriptions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long JDPVehicleInfoId { get; set; }

        public int VehicleID { get; set; }

        public int DealerID { get; set; }

        [MaxLength(100)]
        public string ExtendedVehicleName { get; set; }

        [MaxLength(100)]
        public string ExtendedBodyStyle { get; set; }

        [MaxLength(100)]
        public string ExtendedDrivetrain { get; set; }

        [MaxLength(200)]
        public string ExtendedEngineType { get; set; }

        [MaxLength(100)]
        public string ExtendedDisplacement { get; set; }

        [MaxLength(100)]
        public string ExtendedFuelSystem { get; set; }

        [MaxLength(100)]
        public string ExtendedTransmissionDescripCont { get; set; }

        [MaxLength(100)]
        public string ExtendedTransType { get; set; }

        [MaxLength(100)]
        public string ExtendedBestmakename { get; set; }

        [MaxLength(100)]
        public string ExtendedBestmodelname { get; set; }

        [MaxLength(100)]
        public string ExtendedBeststylename { get; set; }

        [MaxLength(100)]
        public string ExtendedBesttrimname { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }
    }
}
