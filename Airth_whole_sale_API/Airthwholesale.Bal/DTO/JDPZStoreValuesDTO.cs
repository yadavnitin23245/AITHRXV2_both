namespace Airthwholesale.Bal.DTO
{
    public class JDPZStoreValuesDTO
    {
        public int id { get; set; }

        public string StoreName { get; set; }

        public string KeyCategory { get; set; }

        public string KeyName { get; set; }

        public string KeyValue { get; set; }

        public string ForEnvironment { get; set; }

        public string StoreShortCode { get; set; }

        public string ExtValue1 { get; set; }

        public string ExtValue2 { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? DeletedBy { get; set; }
    }
}
