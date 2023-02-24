using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Airthwholesale.Data.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(300)]
        public string? addresslineone { get; set; }

        [StringLength(300)]
        public string? addresslinetwo { get; set; }


        [StringLength(30)]
        public string? cityName { get; set; }

        [StringLength(30)]
        public string? Country { get; set; }

        [StringLength(30)]
        public string? State { get; set; }


        public string? Userid { get; set; }

        public string? trackingRegisterId { get; set; }

        public DateTime? createDate { get; set; }
    }
}
