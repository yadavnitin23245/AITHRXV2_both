using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airthwholesale.Data.Models;

namespace Airthwholesale.Bal.DTO
{
    public class AppUserDTO : AppUser
    {
        public string? addresslineone { get; set; }

        public string? addresslinetwo { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }
        public string? gstNumber { get; set; }
        public string? EFTinfo { get; set; }

    }
}
