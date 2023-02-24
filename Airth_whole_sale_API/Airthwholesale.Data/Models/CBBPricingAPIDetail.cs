namespace Airthwholesale.Data.Models
{
    public class CBBPricingAPIDetail
    {
        public int id { get; set; }

        public string APIName { get; set; }

        public int? PullCount { get; set; }

        public int? VINCountCBBPull { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }

    }

}
