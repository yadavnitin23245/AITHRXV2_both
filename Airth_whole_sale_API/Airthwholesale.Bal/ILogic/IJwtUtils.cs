using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(AppUser user);
        public string? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);

        public string CreateJWTTokenmethod(AppUser appUser);
    }
}
