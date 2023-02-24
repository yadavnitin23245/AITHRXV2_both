using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class CleanImagesDetailsDTO
    {
        public string GroupName { get; set; }

        public string DealerName { get; set; }
        public string StockNumber { get; set; }
        public string VIN { get; set; }

        public string CarYr { get; set; }
        public string Mileage { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }

        public string Body { get; set; }

        public string Colour { get; set; }
        public string Removetext_count { get; set; }

        public int? VehicleID { get; set; }

    }
}
