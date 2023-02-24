using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public string SubscriptionName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Createdby { get; set; }
    }
}
