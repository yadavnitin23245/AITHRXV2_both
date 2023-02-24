namespace Airthwholesale.Data.Models
{
    public class JDPDealerInfo
    {
        public long id { get; set; }

        public int DealerID { get; set; }

        public string DealerName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string Lattitude { get; set; }

        public string Longitude { get; set; }

        public string OfficePhone { get; set; }

        public string Mobile { get; set; }

        public string WebAddress { get; set; }

        public string ContactPersonName1 { get; set; }

        public string ContactPersonName2 { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }

        public bool? IsInternalSynch { get; set; }

        public DateTime? SynchedDate { get; set; }

        public long? SynchedBy { get; set; }

        public long? InternalID { get; set; }
    }
}
