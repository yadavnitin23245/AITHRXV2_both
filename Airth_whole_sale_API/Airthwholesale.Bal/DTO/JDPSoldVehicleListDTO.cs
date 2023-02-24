using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class JDPSoldVehicleListDTO
    {
        public List<LstVehicleDataStockModel> LstVehicleDataStockModel { get; set; }
        public ErrorCode Error { get; set; }
        public DateTime ResponseDate { get; set; }
        public string status { get; set; }
    }

    public class ErrorCode
    {
        public int Code { get; set; }
        public object Message { get; set; }
    }

    public class LstVehicleDataStockModel
    {
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public int VehicleID { get; set; }
        public int ExtraPrice1 { get; set; }
        public int ExtraPrice2 { get; set; }
        public int ExtraPrice3 { get; set; }
    }

}
