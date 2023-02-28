using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class GraphQLDTO
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public GetVehicles getVehicles { get; set; }
    }
    public class GetVehicles
    {
        public List<Vehicle> Vehicles { get; set; }
    }
    public class Vehicle
    {
        public string vin { get; set; }
        public string stock_number { get; set; }
        public int year { get; set; }
        public int kms { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string trim { get; set; }
        public string body_type { get; set; }
        public string exterior_colour { get; set; }
        public double product_price { get; set; }
        public string condition { get; set; }
        public DateTime landed_at { get; set; }
        public string group_name { get; set; }
        public string dealer_name { get; set; }
        public string drivetrain { get; set; }
        public int seats { get; set; }
        public string transmission { get; set; }
        public string condition_status { get; set; }
        public string interior_colour { get; set; }
        //public string vehicle_features { get; set; }
        //public string inspection_report { get; set; }
    }
}
