using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class IICCBatchApiDTO
    {

        public string? ApiBaseUrl { get; set; }
        public string? Apikey { get; set; }

        public string? Dealers { get; set; }
        public string? Pageno { get; set; }
        public string? Format { get; set; }
        public string? vin { get; set; }

        public int? Mileage { get; set; }


        public string? ImagePath { get; set; }
    }
}
