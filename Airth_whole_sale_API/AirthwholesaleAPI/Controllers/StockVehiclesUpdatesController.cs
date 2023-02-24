using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Bal.Logic;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirthwholesaleAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class StockVehiclesUpdatesController : ControllerBase
    {
        #region Private properties
        protected IStockVehiclesUpdatesLogic _IStockVehiclesUpdatesLogic { get; private set; }
        private readonly IRepository<JDPVehicleInfo> _jDPVehicleInfo;
        private readonly IRepository<JDPExtendedDescriptions> _jDPExtendedDescriptions;
        private readonly IRepository<JDPListOfPhotos> _jDPListOfPhotos;
        private readonly IRepository<JDPVehicleComments> _jDPVehicleComments;
        private readonly IRepository<JDPPremiumOptions> _jDPPremiumOptions;
        private readonly IRepository<JDPSubOptions> _jDPSubOptions;
        private readonly IRepository<JDPListOfAppliedOffers> _JDPListOfAppliedOffers;
        private readonly IRepository<JDPListOfOptions> _JDPListOfOptions;
        
        //these code are responsible for call to  Identity 
        private SignInManager<AppUser> _signInManager;
        #endregion


        #region CTOR's
        public StockVehiclesUpdatesController(         
             IRepository<JDPVehicleInfo> jDPVehicleInfo,
             IRepository<JDPExtendedDescriptions> jDPExtendedDescriptions,
             IRepository<JDPListOfPhotos> jDPListOfPhotos,
             IRepository<JDPVehicleComments> jDPVehicleComments,
             IRepository<JDPPremiumOptions> jDPPremiumOptions,
             IRepository<JDPSubOptions> jDPSubOptions,
             IRepository<JDPListOfAppliedOffers> JDPListOfAppliedOffers,
             IRepository<JDPListOfOptions> JDPListOfOptions,
             IStockVehiclesUpdatesLogic IStockVehiclesUpdatesLogic

            )

        {
            _jDPVehicleInfo = jDPVehicleInfo;
            _jDPExtendedDescriptions = jDPExtendedDescriptions;
            _jDPListOfPhotos = jDPListOfPhotos;
            _jDPVehicleComments = jDPVehicleComments;
            _jDPPremiumOptions = jDPPremiumOptions;
            _jDPSubOptions = jDPSubOptions;
            _JDPListOfAppliedOffers = JDPListOfAppliedOffers;
            _JDPListOfOptions = JDPListOfOptions;
            _IStockVehiclesUpdatesLogic = IStockVehiclesUpdatesLogic;
        }
        #endregion

        #region API Methods


        /// <summary>
        /// For getting Get Stock Vehicles Updates list by Date
        /// </summary>
        /// <returns></returns>
        /// 
         [Authorize]
        [HttpPost]
        [Route("GetStockVehiclesUpdatesListByDate")]
        public async Task<IActionResult> GetStockVehiclesUpdatesListByDate([FromBody] JDPSoldVehiclesParametersDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _IStockVehiclesUpdatesLogic.GetStockVehiclesUpdates(Obj);
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
    }
}
