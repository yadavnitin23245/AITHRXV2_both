using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airthwholesale.Data.Models
{
    public class BusinessInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        public string? businessname { get; set; }

        [StringLength(150)]
        public string? gstNumber { get; set; }

      
        public string? EFTinfo { get; set; }

        public string? paymenttype { get; set; }

        public string? Userid { get; set; }

        public string? trackingRegisterId { get; set; }

        public DateTime? createDate { get; set; }
    }
}
