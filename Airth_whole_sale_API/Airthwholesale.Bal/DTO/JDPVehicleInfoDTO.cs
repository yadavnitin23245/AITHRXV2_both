using Airthwholesale.Data.Models;

namespace Airthwholesale.Bal.DTO
{
    public class JDPVehicleInfoDTO : JDPVehicleInfo
    {
        public int ? Totalcount { get; set; }

        public int? VehicleTotalcount { get; set; }

        public string updatestatus { get; set; }
    }
}
