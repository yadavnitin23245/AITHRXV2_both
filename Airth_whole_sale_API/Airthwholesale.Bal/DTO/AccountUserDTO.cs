using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public  class AccountUserDTO
    {
       
        public string firstName { get; set; }

      
        public string lastName { get; set; }

    
        public string phone { get; set; }

        public string email { get; set; }


        public string confirmPassword { get; set; }


        public string password { get; set; }

        public string trackingRegisterId { get; set; }
    }
}
