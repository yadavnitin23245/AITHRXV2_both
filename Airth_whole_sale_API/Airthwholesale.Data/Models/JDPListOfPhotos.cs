using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class JDPListOfPhotos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long JDPVehicleInfoId { get; set; }

        public int VehicleID { get; set; }

        public int DealerID { get; set; }

        [MaxLength(100)]
        public string VehiclePhotoID { get; set; }

        [MaxLength(200)]
        public string PhotoUrl { get; set; }

        public int Order { get; set; }

        public DateTime PhotoTimestamp { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }

        public string? CleanedPhotourl { get; set; }

        public string? RemovetextCDNlink { get; set; }
    }
}
