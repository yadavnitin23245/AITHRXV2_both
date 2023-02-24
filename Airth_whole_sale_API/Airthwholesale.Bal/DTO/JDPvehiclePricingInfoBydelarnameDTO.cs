using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class JDPvehiclePricingInfoBydelarnameDTO
    {
        public string Vin { get; set; }
        public DateTime InStockDate { get; set; }

        public string VehicleStatus { get; set; }
        public bool IsNew { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string StockNumber { get; set; }

        public string ExteriorColor { get; set; }
        public string Trim { get; set; }
        public object BodyName { get; set; }
        public string BodyStyle { get; set; }
        public string Delearname { get; set; }

        public long Delearid { get; set; }


        // FROM JDP PRICING
        public int Cost { get; set; }
        public int List { get; set; }
        public int Special { get; set; }
        public int ExtraPrice1 { get; set; }
        public int ExtraPrice2 { get; set; }
        public int ExtraPrice3 { get; set; }

        // for CBB values
        public int Adjustedwholeavg { get; set; }

        public int Adjustedwholerough { get; set; }

        public int Adjustedwholexclean { get; set; }

        public int Adjustedwholeclean { get; set; }
    }
}
