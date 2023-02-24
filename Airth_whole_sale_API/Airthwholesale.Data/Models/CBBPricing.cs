namespace Airthwholesale.Data.Models
{
    public class CBBPricing
    {
        public int id { get; set; }

        public long? JDPVehicleInfoId { get; set; }

        public int? VehicleID { get; set; }

        public int? DealerID { get; set; }

        public string VIN { get; set; }

        public int? Adjustedwholeavg { get; set; }

        public int? Adjustedwholerough { get; set; }

        public int? Adjustedwholexclean { get; set; }

        public int? Adjustedwholeclean { get; set; }

        public string Year { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Series { get; set; }
        public string Style { get; set; }
        public string ClassName { get; set; }

        public string Trim { get; set; }

        public int? Order { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }

    }

}
