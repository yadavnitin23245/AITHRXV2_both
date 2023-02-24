using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class JDPSoldVehiclesParametersDTO
    {

        public string ApiBaseUrl { get; set; }
        public string Apikey { get; set; }

        public string Dealers { get; set; }
        public string startDate { get; set; }
        public string Format { get; set; }
    }
}
