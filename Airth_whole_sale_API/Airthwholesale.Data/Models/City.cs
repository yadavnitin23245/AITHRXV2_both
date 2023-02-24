using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Data.Models
{
    public class City
    {
        public int id { get; set; }

        public int? Stateid { get; set; }


        [MaxLength]
        public string Name { get; set; }

        public int? Countryid { get; set; }
    }
}
