using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data.Models;
using AirthwholesaleAPI.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System.Web;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirthwholesaleAPI.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {

   
        #region Private properties

        //these code are responsible for call to  Identity 
    
        public IConfiguration _configurationObj;
        protected IUserLogic _userLogicBAL { get; private set; }
        //these code are responsible for call to  Identity 
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private readonly IOptions<AppSettingsDTO> _appSettings;
        public IConfiguration _configuration;

        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        #endregion



        #region CTOR's
        public AccountController(IUserLogic userLogicBAL, UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr, 
            IOptions<AppSettingsDTO> appSettings, IConfiguration config, IICCBatchLogic IICCBatchLogicBAL)

        {
            _userLogicBAL = userLogicBAL;
            userManager = userMgr;
            signInManager = signinMgr;
            _appSettings = appSettings;
            _configuration = config;
            _configurationObj = config;
            _IICCBatchLogicBAL = IICCBatchLogicBAL;

        }
        #endregion


        #region CURD For User Table
 
        [HttpGet]
        [Route("all")]

         public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _userLogicBAL.GetAll();
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion


        #region Login Related Functions
        /// <summary>
        /// Functions for  ForgotPassword
        /// </summary>
        /// <returns></returns>
        ///

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] CustomParametersDTO emaiParam)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }


                var user = await userManager.FindByEmailAsync(emaiParam.email);
                if (user == null)
                {
                    return BadRequest();
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                //var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

                var newToken = HttpUtility.UrlEncode(token);
                var link = string.Format(_appSettings.Value.ResetpasswordURL + "{0}/{1}", newToken, emaiParam.email);

                EmailHelper emailHelper = new EmailHelper(_appSettings, _IICCBatchLogicBAL);
                bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

                if (emailResponse)
                {
                    return Ok();
                }
                else
                {
                    // log email failed 
                    return BadRequest();
                }
                return Ok();
            }
            catch (AppException ex)
            {
                return null;
            }
        }



        /// <summary>
        //This code call when user will click on link from their mail for reset
        /// </summary>
        /// <returns></returns>
        ///
        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest();
            }

            return Ok();
        }



        /// <summary>
        ////Register User
        /// </summary>
        /// <returns></returns>
        ///

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Create([FromBody] RegisterUserDTO user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = new AppUser
                    {
                        UserName = user.Name,
                        Email = user.Email
                    };

                    IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                        var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                        EmailHelper emailHelper = new EmailHelper(_appSettings, _IICCBatchLogicBAL);
                        bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                        if (emailResponse)
                        {
                            return Ok(user);
                        }
                        else
                        {
                            // log email failed 
                        }
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest("message");
                    }
                }
                return Ok(user);
            }
            catch (AppException ex)
            {
                return null;
            }
        }



        /// <summary>
        ////Register User without confirmation email.
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterWithoutmailconfirmation")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trackingRegisterId = Guid.NewGuid().ToString();
                    if (user.userlist.Count > 0)
                    {
                        //saving multiple user
                        foreach (var userdata in user.userlist)
                        {

                            if(userdata.password!=userdata.confirmPassword)
                            {
                                return BadRequest("Password did not matched");
                            }

                            AppUser appUser = new AppUser
                            {
                                UserName = userdata.email,
                                Email = userdata.email,
                                firstName = userdata.firstName,
                                lastName = userdata.lastName,
                                phone = userdata.phone,
                                PhoneNumber= userdata.phone,
                                trackingRegisterId = trackingRegisterId,
                                Userrole="Admin" 
                            };

                            IdentityResult result = await userManager.CreateAsync(appUser, userdata.password);
                            if (result.Succeeded)
                            {
                                //return Ok(user);
                            }
                            else
                            {
                                foreach (IdentityError error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return BadRequest(result.Errors.FirstOrDefault().Description);
                            }
                        }

                        //saving business data and tracking id which will be use for linq all table

                        BusinessInfo businesobj = new BusinessInfo();
                        businesobj.trackingRegisterId = trackingRegisterId;
                        businesobj.businessname = user.businessname;
                        businesobj.gstNumber = user.gstNumber;
                        businesobj.EFTinfo = user.EFTinfo;
                        businesobj.paymenttype = user.paymenttype;

                       var savestatus= await _userLogicBAL.SaveBusinessInfo(businesobj);


                        //saving address against save user
                        Address Addressobj = new Address();
                        Addressobj.trackingRegisterId = trackingRegisterId;
                        Addressobj.addresslineone = user.addresslineone;
                        Addressobj.addresslinetwo = user.addresslinetwo;
                        Addressobj.cityName = user.cityName;
                        Addressobj.Country = user.Country;
                        Addressobj.State = user.State;

                        var savestatusAddress = await _userLogicBAL.SaveAddress(Addressobj);
                        return Ok(user);
                    }
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.ToString());
                }
               
            }
            return Ok(user);
        }




        /// <summary>
        ////Login user Function
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, true);
                    if (result.Succeeded)
                    {

                        //create claims details based on the user information
                        #pragma warning disable CS8604 // Possible null reference argument.
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", appUser.UserName),
                        new Claim("Email", appUser.Email)
                    };
                        #pragma warning restore CS8604 // Possible null reference argument.

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        var UserName= appUser.firstName + "  " + appUser.lastName;
                        return Ok(new LoginDTO { jwtToken = tokenString, Email = login.Email, ReturnUrl = login.ReturnUrl, Role = appUser.Userrole, UserName = UserName });
                        //  return Ok(login.ReturnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
                        return Unauthorized(new { LoginError = "Please check your Login Credentials" });
                    }
                    /*bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                    }*/


                }
                else
                {
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
                    return Unauthorized(new { LoginError = "Please check your Login Credentials" });
                }

            }
            return Ok(login);
        }




        /// <summary>
        ////Login user Function  with two step verification where user will enter own email and twofactor code will sent to enter mail
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [AllowAnonymous]
        [Route("LoginTwoStepGet")]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailHelper emailHelper = new EmailHelper(_appSettings, _IICCBatchLogicBAL);
            bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return Ok("DONE");
        }



        /// <summary>
        ////Login user Function  with email and twofactor code
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [Route("LoginTwoStep")]
        public async Task<IActionResult> LoginTwoStep(TwoFactorDTO twoFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
               // return Redirect(twoFactor.returnUrl ?? "/");
                return Ok(twoFactor.returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return Ok();
            }
        }
        #endregion

    }
}
