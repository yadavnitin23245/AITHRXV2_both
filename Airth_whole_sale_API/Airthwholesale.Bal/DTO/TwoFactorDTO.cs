using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class TwoFactorDTO
    {
        public string TwoFactorCode { get; set; }

        public string returnUrl { get; set; }
    }
}
