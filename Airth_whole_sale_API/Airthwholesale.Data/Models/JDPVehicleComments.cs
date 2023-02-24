using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class JDPVehicleComments
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public long JDPVehicleInfoId { get; set; }
        public int? VehicleID { get; set; }

        public int? DealerID { get; set; }

        [MaxLength(1000)]
        public string Comments { get; set; }

        [MaxLength(1000)]
        public string Comments2 { get; set; }

        [MaxLength(1000)]
        public string Comments3 { get; set; }

        [MaxLength(1000)]
        public string Comments4 { get; set; }

        [MaxLength(1000)]
        public string Comments5 { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
