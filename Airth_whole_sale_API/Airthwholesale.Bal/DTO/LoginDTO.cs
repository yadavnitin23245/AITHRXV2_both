using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class LoginDTO
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }

        public string? jwtToken { get; set; }


         public string? Role { get; set; }

        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public AppUser? userdata { get; set; }
        
    }
}
