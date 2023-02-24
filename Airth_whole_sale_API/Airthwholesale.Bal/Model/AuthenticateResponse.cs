using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.Model
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(AppUser user, string jwtToken )
        {
            Id = user.Id;
            FirstName = user.firstName;
            LastName = user.lastName;
            Username = user.UserName;
            JwtToken = jwtToken;
            role = user.Userrole;
          //  RefreshToken = refreshToken;
        }

        public string role { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
