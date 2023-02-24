namespace Airthwholesale.Data.Models
{
    public class DGroup
    {
        public int id { get; set; }

        public string GroupName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Createdby { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdatedBy { get; set; }

    }

}
