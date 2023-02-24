using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class JDPSearchDTO
    {
        public string DealerId { get; set; }

        public string Make { get; set; }

        public int Opcode { get; set; }

        public int? PageNumber { get; set; }

        public int? RowsOfPage { get; set; }

    }

}
