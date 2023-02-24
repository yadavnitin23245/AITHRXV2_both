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
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirthwholesaleAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class SoldVehiclesController : ControllerBase
    {


        #region Private properties
      
        protected ISoldVehiclesLogic _ISoldVehiclesLogicBAL { get; private set; }
        private readonly IRepository<JDPVehicleInfo> _jDPVehicleInfo;
        private readonly IRepository<JDPExtendedDescriptions> _jDPExtendedDescriptions;
        private readonly IRepository<JDPListOfPhotos> _jDPListOfPhotos;
        private readonly IRepository<JDPVehicleComments> _jDPVehicleComments;
        private readonly IRepository<JDPPremiumOptions> _jDPPremiumOptions;
        private readonly IRepository<JDPSubOptions> _jDPSubOptions;
        private readonly IRepository<JDPListOfAppliedOffers> _JDPListOfAppliedOffers;
        private readonly IRepository<JDPListOfOptions> _JDPListOfOptions;
        
      
        #endregion

        #region CTOR's
        public SoldVehiclesController(
            ISoldVehiclesLogic ISoldVehiclesLogicBAL,
             IRepository<JDPVehicleInfo> jDPVehicleInfo,
             IRepository<JDPExtendedDescriptions> jDPExtendedDescriptions,
             IRepository<JDPListOfPhotos> jDPListOfPhotos,
             IRepository<JDPVehicleComments> jDPVehicleComments,
             IRepository<JDPPremiumOptions> jDPPremiumOptions,
             IRepository<JDPSubOptions> jDPSubOptions,
             IRepository<JDPListOfAppliedOffers> JDPListOfAppliedOffers,
             IRepository<JDPListOfOptions> JDPListOfOptions

            )

        {
           
            _ISoldVehiclesLogicBAL = ISoldVehiclesLogicBAL;
            _jDPVehicleInfo = jDPVehicleInfo;
            _jDPExtendedDescriptions = jDPExtendedDescriptions;
            _jDPListOfPhotos = jDPListOfPhotos;
            _jDPVehicleComments = jDPVehicleComments;
            _jDPPremiumOptions = jDPPremiumOptions;
            _jDPSubOptions = jDPSubOptions;
            _JDPListOfAppliedOffers = JDPListOfAppliedOffers;
            _JDPListOfOptions = JDPListOfOptions;
        }
        #endregion

        #region API Methods

        /// <summary>
        /// For getting Get Sold Vehicles list By Date 
        /// </summary>
        /// <returns></returns>
       //  [Authorize]
        [HttpPost]
        [Route("GetSoldVehiclesByDates")]
        public async Task<IActionResult> GetSoldVehiclesByDates([FromBody] JDPSoldVehiclesParametersDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = await _ISoldVehiclesLogicBAL.GetSoldVehiclesByDate(Obj);



                if(responseObj!=null)
                {
                    var MainXML = new XElement("DocumentElement");
                    foreach (var Listdata in responseObj.LstVehicleDataStockModel)
                    {
                            var datacontent =
                                            new XElement("Soldvehicle",
                                            new XElement("VIN", Listdata.ExtraPrice1),
                                            new XElement("StockNumber", Listdata.ExtraPrice2),
                                           new XElement("VehicleID", Listdata.ExtraPrice2),
                                            new XElement("ExtraPrice1", Listdata.ExtraPrice2),
                                             new XElement("ExtraPrice2", Listdata.ExtraPrice2),
                                              new XElement("ExtraPrice3", Listdata.ExtraPrice2)
                                            );
                            MainXML.Add(datacontent);
                        
                    }
                    if (MainXML.ToString() == "<DocumentElements/>")
                    {
                        
                    }
                    else
                    {
                        _ISoldVehiclesLogicBAL.UpdateSoldpricebyAPIIndatabase(MainXML.ToString());
                    }
                }

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
