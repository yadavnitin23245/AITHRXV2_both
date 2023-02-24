namespace Airthwholesale.Data.Models
{
    public class JDPAPICallHistory
    {
        public long id { get; set; }

        public int DealerID { get; set; }

        public string APIName { get; set; }

        public string APICallDetails { get; set; }

        public string InitiatedFromIP { get; set; }

        public DateTime? InitiatedTimeStamp { get; set; }

        public DateTime? CompletedTimeStamp { get; set; }

        public string Status { get; set; }

        public string ResponseFileLocationLink { get; set; }

        public int? PageCount { get; set; }

        public int? RecordCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public bool? IsArchived { get; set; }

        public DateTime? ArchivedDate { get; set; }

        public long? ArchivedBy { get; set; }
    }
}
