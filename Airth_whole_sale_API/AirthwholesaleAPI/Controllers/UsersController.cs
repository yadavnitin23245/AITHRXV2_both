using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Bal.Model;
using Airthwholesale.Data.Models;
using AirthwholesaleAPI.Authorization;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirthwholesaleAPI.Controllers
{
   
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected IUserLogic _userService { get; private set; }
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public IConfiguration _configuration;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        public UsersController(IUserLogic userService, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
             IConfiguration config, IICCBatchLogic iICCBatchLogicBAL )
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = config;
            _IICCBatchLogicBAL = iICCBatchLogicBAL;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]

        public async Task<IActionResult> AuthenticateAsync(LoginDTO model)
        {


            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.Jwt.ToString();
            string KeyCategory = ZStoreKeyCategory.JwtValues.ToString();
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.JwtValues.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            var tokenString = "";
            AppUser appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, false, true);
                if (result.Succeeded)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                   {
                        new Claim("UserID",appUser.Id.ToString())
                   }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APIValuesList.JWT_SecretValue)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    tokenString = tokenHandler.WriteToken(securityToken);
                }

                var UserName = appUser.firstName + "  " + appUser.lastName;
                return Ok(new LoginDTO { jwtToken = tokenString, Email = model.Email, ReturnUrl = model.ReturnUrl, Role = appUser.Userrole, UserName = UserName,userdata= appUser });
            }

                    return Ok(tokenString);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        // helper methods
        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }



        //consuming api process
        // Method to authenticate user login
        [AllowAnonymous]
        [HttpPost]
        [Route("ConsumeApi")]
        public async Task<IActionResult> ConsumeApiAsync()
        {
            dynamic runTimeObject = new ExpandoObject();
            runTimeObject.Email = "aithradmin@aithr.ca";
            runTimeObject.Password = "Spadez@123";

            try
            {

                var apiBaseUrl = "http://devairthwholesaleapi.spadez.co/api";
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(runTimeObject), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/Users/authenticate";
                    using (var Response = await client.PostAsync(endpoint, content))
                    {
                        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var customerJsonString = await Response.Content.ReadAsStringAsync();
                            // dynamic stuff = JsonConvert.DeserializeObject(customerJsonString);
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            dynamic item = serializer.Deserialize<object>(customerJsonString);
                            string jwttoken = item["jwtToken"];
                         var getdata=   AccapiWithpassingTokenAsync(jwttoken);
                            // var convertedata = JsonConvert.SerializeObject(user);
                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                            return Ok();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); 
            }

            return null;
        }

        //call api with passing parameters
        [HttpGet]
        public async Task AccapiWithpassingTokenAsync(string jwttoken)
        {
            dynamic runTimeObject = new ExpandoObject();
            runTimeObject.dealers = "195641";
            var apiBaseUrl = "http://devaithrwholesaleapi.spadez.co/api";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(runTimeObject), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/ICCBatchApi/GetVehiclesByDealerNames";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwttoken);
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var customerJsonString = await Response.Content.ReadAsStringAsync();
                    }
                }
            }
        }


    }
}
