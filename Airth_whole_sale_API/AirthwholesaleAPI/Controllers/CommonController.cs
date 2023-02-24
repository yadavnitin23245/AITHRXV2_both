using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data.Models;
using AirthwholesaleAPI.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirthwholesaleAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class CommonController : ControllerBase
    {


        #region Private properties

        private IHttpContextAccessor _accessor;

        //these code are responsible for call to  Identity 
        protected ICommonLogic _commonLogicBAL { get; private set; }

        protected IUserLogic _userLogicBAL { get; private set; }
        private SignInManager<AppUser> _signInManager;

     
        #endregion

        #region CTOR's

        public CommonController(ICommonLogic commonLogicBAL, IUserLogic userLogicBAL, SignInManager<AppUser> signinMgr, IHttpContextAccessor accessor)

        {
            _commonLogicBAL = commonLogicBAL;
            _userLogicBAL = userLogicBAL;
            _signInManager = signinMgr;
            _accessor = accessor;
        }
        #endregion


        /// <summary>
        /// For getting Get user list
        /// </summary>
        /// <returns></returns>
        /// 

      //  [Authorize]
        [HttpGet]
        [Route("Getuserlist")]
        public IActionResult GetAlluser()
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

        /// <summary>
        /// For getting Get Client IP Address
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        public string GetClientIPAddress(HttpContext context)
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
            {
                ip = context.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// For getting Get Countries list
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [AllowAnonymous]
        [Route("GetCountries")]
        public IActionResult GetAll()
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _commonLogicBAL.GetCountryList();
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet]
        [Route("Getipdata")]
        public IActionResult Getipdata()
        {
            var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            return Ok(""); ;
        }


        /// <summary>
        /// For getting Get State list by Countryid
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("GetStateListByCountryIds")]
        public IActionResult GetStateListByCountryIds([FromBody] CountryDTO countryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _commonLogicBAL.GetStateListByCountryId(Convert.ToInt32(countryDTO.id));
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// For getting Get City list by state and Countryid
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("GetCityListByCountryIds")]
        public IActionResult GetCityListByCountryIds([FromBody] StateDTO stateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _commonLogicBAL.GetCityListByCountryId(Convert.ToInt32(stateDTO.Countryid), Convert.ToInt32(stateDTO.id));
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Add Users
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _userLogicBAL.AddUser(Obj);
                if (string.IsNullOrEmpty(responseObj))
                {
                    return NotFound();
                }
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// For getting Get User detail by user id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetUserDetailById")]
        public IActionResult GetUserDetailById([FromBody] UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _userLogicBAL.GetUserDetailByIds(userDTO.UserName.ToString());
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates Users
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _userLogicBAL.UpdateUser(Obj);
                if (string.IsNullOrEmpty(responseObj))
                {
                    return NotFound();
                }
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// For update user detail
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("updateUserDetail")]
        public async Task<IActionResult> UpdateUserDetail([FromBody] AppUserDTO appUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AppUserDTO obj = new AppUserDTO();
                obj.Email = appUserDTO.Email;
                var responseObj = await _userLogicBAL.Update(appUserDTO);
                if (string.IsNullOrEmpty(responseObj))
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// For getting Delete user details
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("DeleteUser/{email}")]
        public async Task<IActionResult> deleteAspUsers(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AppUserDTO obj = new AppUserDTO();
                obj.Email = email;
                var responseObj = await _userLogicBAL.Delete(email);
                return Ok(obj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete Global Codes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _userLogicBAL.DeleteUser(id);
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("UpdateDealer")]
        public async Task<IActionResult> UpdateDealer(int dealerId, string jwtToken)
        {

            try { 
               string apiBaseUrl = "http://devaithrwholesaleapi.spadez.co/api";

        dynamic runTimeObject = new ExpandoObject();
            runTimeObject.dealers = dealerId.ToString();
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(runTimeObject), Encoding.UTF8, "application/json");
                    string endPoint = apiBaseUrl + "/ICCBatchApi/GetVehiclesByDealerNames";
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
                    using (var Response = await client.PostAsync(endPoint, content))
                    {
                        if (Response.StatusCode == HttpStatusCode.OK)
                        {
                            runTimeObject = await Response.Content.ReadAsStringAsync();
                            var document = JsonConvert.DeserializeObject(runTimeObject);
                            return document.ToString();
                        }
                        return Ok();
                    }

                }
                
            }
            catch(Exception ex)
            {

            }
            return null;
        }

    }
}
