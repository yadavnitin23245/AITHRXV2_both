using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.Logic
{
    public class JwtUtils : IJwtUtils
    {
        private AirthwholesaleDbContext _context;
        private readonly AppSettingsDTO _appSettings;
        public IConfiguration _configuration;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }

        public JwtUtils(
            AirthwholesaleDbContext context,
            IOptions<AppSettingsDTO> appSettings,
            IConfiguration configuration, IICCBatchLogic iICCBatchLogicBAL
            )
        {
            _context = context;
            _appSettings = appSettings.Value;
            _configuration = configuration;
            _IICCBatchLogicBAL= iICCBatchLogicBAL;
        }

        public string GenerateJwtToken(AppUser user)
        {

            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public string CreateJWTTokenmethod(AppUser appUser)
        {
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.Jwt.ToString();
            string KeyCategory = ZStoreKeyCategory.JwtValues.ToString();
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.JwtValues.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            //create claims details based on the user information
            var claims = new[] {
                        //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                         new Claim(JwtRegisteredClaimNames.Sub, APIValuesList.SubjectValue),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", appUser.UserName),
                        new Claim("Email", appUser.Email)
                    };
               

                 var key = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(APIValuesList.KeyValue)

                );
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                APIValuesList.IssuerValue,
                APIValuesList.AudienceValue,
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public string? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.Jwt.ToString();
            string KeyCategory = ZStoreKeyCategory.JwtValues.ToString();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.JwtValues.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);


            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:JWT_Secret"]);
            var key = Encoding.ASCII.GetBytes(APIValuesList.JWT_SecretValue);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var userId = jwtToken.Claims.First(x => x.Type == "UserID").Value;
                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                // token is valid for 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = !_context.User.Any(u => u.RefreshTokens.Any(t => t.Token == token));

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }
    }
}
