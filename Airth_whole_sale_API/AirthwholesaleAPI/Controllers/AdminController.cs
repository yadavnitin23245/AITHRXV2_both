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
    public class AdminController : ControllerBase
    {


        #region Private properties

        private IHttpContextAccessor _accessor;

        //these code are responsible for call to  Identity 
        protected IAdminLogic _adminLogicBAL { get; private set; }

     

     
        #endregion

        #region CTOR's

        public AdminController(IAdminLogic adminLogicBAL)

        {
            _adminLogicBAL = adminLogicBAL;
           
        }
        #endregion



        #region Groups
        /// <summary>
        /// For getting Get Group list
        /// </summary>
        /// <returns></returns>
        /// 

        //  [Authorize]
        [HttpGet]
        [Route("GetAllGroupsList")]
        public IActionResult GetAllGroupsList()
        {
            try
            {
               

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetAllGroups();
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
        /// Add Groups
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddGroup")]
        public async Task<IActionResult> AddGroup([FromBody] DGroupDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.AddGroups(Obj);
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
        [Route("GetGroupById")]
        public IActionResult GetGroupById([FromBody] DGroupDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetGroupByIds(obj.id.ToString());
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj.FirstOrDefault());
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
        [Route("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateDTO objparam)
        {
            try
            {
               
                DGroupDTO objectupdategroup= new DGroupDTO();
                objectupdategroup.id = objparam.id;
                objectupdategroup.GroupName = objparam.GroupName;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.UpdateGroups(objectupdategroup);
                if (string.IsNullOrEmpty(responseObj))
                {
                    return NotFound();
                }
                return Ok(new
                {
                    data = responseObj
                });
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
        [HttpPost]
        [Route("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup([FromBody] DeleteDTO Obj)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.DeleteGroups(Obj.Id);
                if (string.IsNullOrEmpty(responseObj))
                {
                    return NotFound();
                }

             
                
                return Ok(new
                {
                    data= responseObj
                });
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("DeleteGroup/{id}")]
        public async Task<IActionResult> oldDeleteGroup(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.DeleteGroups(id);
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

        #region Roles

        ////  [Authorize]
        //[HttpGet]
        //[Route("GetAllRolesList")]
        //public IActionResult GetAllRolesList()
        //{
        //    try
        //    {


        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = _adminLogicBAL.GetAllRoles();
        //        if (responseObj == null)
        //            return NotFound();
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {


        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPost]
        //[Route("AddRole")]
        //public async Task<IActionResult> AddRole([FromBody] AspNetRolesDTO Obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.AddRoles(Obj);
        //        if (string.IsNullOrEmpty(responseObj))
        //        {
        //            return NotFound();
        //        }
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPost]
        //[Route("GetRolesById")]
        //public IActionResult GetRolesById([FromBody] AspNetRolesDTO obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = _adminLogicBAL.GetRolesByIds(obj.Id.ToString());
        //        if (responseObj == null)
        //            return NotFound();
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpPost]
        //[Route("UpdateRole")]
        //public async Task<IActionResult> UpdateRole([FromBody] AspNetRolesDTO Obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.UpdateRoles(Obj);
        //        if (string.IsNullOrEmpty(responseObj))
        //        {
        //            return NotFound();
        //        }
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpGet]
        //[Route("DeleteRole/{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.DeleteRoles(id);
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        #endregion

        #region Users
        ////  [Authorize]
        //[HttpGet]
        //[Route("GetAllUsersList")]
        //public IActionResult GetAllUsersList()
        //{
        //    try
        //    {


        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = _adminLogicBAL.GetAllUsers();
        //        if (responseObj == null)
        //            return NotFound();
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {


        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPost]
        //[Route("AddUser")]
        //public async Task<IActionResult> AddUser([FromBody] AspNetUsersDTO Obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.AddUsers(Obj);
        //        if (string.IsNullOrEmpty(responseObj))
        //        {
        //            return NotFound();
        //        }
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpPost]
        //[Route("GetUsersById")]
        //public IActionResult GetUsersById([FromBody] AspNetUsersDTO obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = _adminLogicBAL.GetUsersByIds(obj.Id.ToString());
        //        if (responseObj == null)
        //            return NotFound();
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpPost]
        //[Route("UpdateUser")]
        //public async Task<IActionResult> UpdateUser([FromBody] AspNetUsersDTO Obj)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.UpdateUsers(Obj);
        //        if (string.IsNullOrEmpty(responseObj))
        //        {
        //            return NotFound();
        //        }
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //[HttpGet]
        //[Route("DeleteUser/{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var responseObj = await _adminLogicBAL.DeleteUsers(id.ToString());
        //        return Ok(responseObj);
        //    }
        //    catch (AppException ex)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //}
        #endregion

        #region Dealers
        [HttpGet]
        [Route("GetAllDealer")]
        public IActionResult GetAllDealer()
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetAllDealers();
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {


                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddDealer")]
        public async Task<IActionResult> AddDealer([FromBody] DealersListDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.AddDealers(Obj);
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

        [HttpPost]
        [Route("GetDealersById")]
        public IActionResult GetDealersById([FromBody] DealersListDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetDealersByIds(obj.id.ToString());
                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateDealer")]
        public async Task<IActionResult> UpdateDealer([FromBody] DealersListDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.UpdateDealers(Obj);
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

        [HttpGet]
        [Route("DeleteDealer/{id}")]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.DeleteDealers(id);
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        #endregion

        #region Subscription

        [HttpGet]
        [Route("GetAllSubscriptions")]
        public IActionResult GetAllSubscriptions()
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetAllSubscription();
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

        #region AithrPositions

        [HttpGet]
        [Route("GetAllPositionsList")]
        public IActionResult GetAllPositionsList()
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetAllPositions();
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
        /// Add Positions
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPosition")]
        public async Task<IActionResult> AddPosition([FromBody] AithrPositionsDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.AddPositions(Obj);
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
        /// For getting Get Positions detail by user id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetPositionsById")]
        public IActionResult GetPositionsById([FromBody] AithrPositionsDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _adminLogicBAL.GetPositionsByIds(obj.id.ToString());
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
        /// Updates Positions
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePosition")]
        public async Task<IActionResult> UpdatePosition([FromBody] AithrPositionsDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.UpdatePositions(Obj);
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
        /// For getting Delete Positions details
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("DeletePosition/{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _adminLogicBAL.DeletePositions(id);
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}
