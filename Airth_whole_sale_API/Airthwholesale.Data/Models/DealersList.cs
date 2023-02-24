using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Data.Models
{
    public class DealersList
    {

        public int id { get; set; }

        public string DealerName { get; set; }

        public string DealerCity { get; set; }

        public string DealerRegion { get; set; }

        public string DealerAddress { get; set; }

        public string PostalCode { get; set; }

        public DateTime? ActiveUntil { get; set; }

        public DateTime? ActiveStart { get; set; }

        public string DealerPhone { get; set; }

        public int? DGroupId { get; set; }

        public string GstNumber { get; set; }

        public string DealerEmail { get; set; }

        public bool? IstermsAccepted { get; set; }

        public DateTime? TermAcceptedDate { get; set; }

        public string Aggrementlink { get; set; }

        public string SystemIP { get; set; }

        public int? IsTrial { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
