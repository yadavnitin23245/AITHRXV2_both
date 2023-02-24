using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.DTO.StockVehiclesUpdate;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Authorization;
using AirthwholesaleAPI.Common.Enums;
using AirthwholesaleAPI.Email;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using System.Xml.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirthwholesaleAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ICCBatchApiController : ControllerBase
    {

        private readonly IMapper _mapper;



        #region Private properties
        protected ICommonLogic _commonLogicBAL { get; private set; }
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        private readonly IRepository<JDPVehicleInfo> _jDPVehicleInfo;
        private readonly IRepository<JDPExtendedDescriptions> _jDPExtendedDescriptions;
        private readonly IRepository<JDPListOfPhotos> _jDPListOfPhotos;
        private readonly IRepository<JDPVehicleComments> _jDPVehicleComments;
        private readonly IRepository<JDPPremiumOptions> _jDPPremiumOptions;
        private readonly IRepository<JDPSubOptions> _jDPSubOptions;
        private readonly IRepository<JDPListOfAppliedOffers> _JDPListOfAppliedOffers;
        private readonly IRepository<JDPListOfOptions> _JDPListOfOptions;
        private readonly IRepository<JDPPricing> _jDPPricing;
        private readonly IOptions<AppSettingsDTO> _appSettings;
        private readonly IRepository<CBBPricing> _cBBPricing;


        private readonly JDPAPIDbContext _JDPcontext;

        //these code are responsible for call to  Identity 
        private SignInManager<AppUser> _signInManager;
        #endregion


        #region CTOR's
        public ICCBatchApiController(
            SignInManager<AppUser> signinMgr,
            ICommonLogic commonLogicBAL,
            IICCBatchLogic IICCBatchLogicBAL,
             IRepository<JDPVehicleInfo> jDPVehicleInfo,
             IRepository<JDPExtendedDescriptions> jDPExtendedDescriptions,
             IRepository<JDPListOfPhotos> jDPListOfPhotos,
             IRepository<JDPVehicleComments> jDPVehicleComments,
             IRepository<JDPPremiumOptions> jDPPremiumOptions,
             IRepository<JDPSubOptions> jDPSubOptions,
             IRepository<JDPListOfAppliedOffers> JDPListOfAppliedOffers,
             IRepository<JDPListOfOptions> JDPListOfOptions,
             IRepository<JDPPricing> jDPPricing,
             IRepository<CBBPricing> cBBPricing,
             JDPAPIDbContext JDPcontext,
             IMapper mapper,
             IOptions<AppSettingsDTO> appSettings

            )

        {
            _signInManager = signinMgr;
            _commonLogicBAL = commonLogicBAL;
            _IICCBatchLogicBAL = IICCBatchLogicBAL;
            _jDPVehicleInfo = jDPVehicleInfo;
            _jDPExtendedDescriptions = jDPExtendedDescriptions;
            _jDPListOfPhotos = jDPListOfPhotos;
            _jDPVehicleComments = jDPVehicleComments;
            _jDPPremiumOptions = jDPPremiumOptions;
            _jDPSubOptions = jDPSubOptions;
            _JDPListOfAppliedOffers = JDPListOfAppliedOffers;
            _JDPListOfOptions = JDPListOfOptions;
            _jDPPricing = jDPPricing;
            _cBBPricing = cBBPricing;
            _JDPcontext = JDPcontext;
            _appSettings = appSettings;
            _mapper = mapper;

        }
        #endregion


        /// <summary>
        ///Get Vehicle Photos List By VehicleId
        /// </summary>
        /// <returns></returns>
        ///
        [Authorize]
        [HttpPost]
        [Route("GetVehiclePhotosListByVehicleId")]
        public IActionResult GetVehiclePhotosListByVehicleId([FromBody] JDPListOfPhotosDTO jDPListOfPhotosDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehiclePhotosByVehicleIds(jDPListOfPhotosDTO.VehicleID.ToString());

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
        ///GetVehicle Extended Description List By VehicleId
        /// </summary>
        /// <returns></returns>
        /// 

        [Authorize]
        [HttpPost]
        [Route("GetVehicleExtendedDescriptionListByVehicleId")]
        public IActionResult GetVehicleExtendedDescriptionListByVehicleId([FromBody] JDPExtendedDescriptionsDTO jDPExtendedDescriptionsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehicleExtendedDescriptionByVehicleIds(jDPExtendedDescriptionsDTO.VehicleID.ToString());

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
        ///Get Vehicle PremiumOptionsList By Vehicle Id
        /// </summary>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost]
        [Route("GetVehiclePremiumOptionsListByVehicleId")]
        public IActionResult GetVehiclePremiumOptionsListByVehicleId([FromBody] JDPPremiumOptionsDTO jDPPremiumOptionsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehiclePremiumOptionsByVehicleIds(jDPPremiumOptionsDTO.VehicleID.ToString());

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
        /// GetVehicle Comments ListBy Vehicle Id
        /// </summary>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost]
        [Route("GetVehicleCommentsListByVehicleId")]
        public IActionResult GetVehicleCommentsListByVehicleId([FromBody] JDPVehicleComments jDPVehicleComments)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehicleCommentsByVehicleIds(jDPVehicleComments.VehicleID.ToString());

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
        /// Get All Vehicle Details
        /// </summary>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost]
        [Route("GetAllVehicleDetails")]
        public IActionResult GetAllVehicleDetails([FromBody] IICCBatchApiDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetAllVehicleDetail(Obj);

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
        [Route("GetRemovedBgImage")]
        public IActionResult GetRemovedBgImage([FromBody] IICCBatchApiDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var responseObj = _IICCBatchLogicBAL.RemoveBackkgroudImage(Obj);
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
        /// Get Vehicles By Dealer Names for Updating Cost ,Special in JDP Pricing table , Vehicle Status  in  JDPVehicleInfo
        /// </summary>
        /// <returns></returns>
        /// 
     //   [Authorize]
        [HttpPost]
        [Route("GetVehiclesByDealerNames")]
        public async Task<IActionResult> GetVehiclesByDealerNames([FromBody] IICCBatchApiDTO Obj)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string DealerId = Obj.Dealers.ToString();
                // for getting Response from API and this is used for update.
                var responseObj = await _IICCBatchLogicBAL.GetVehiclesByDealerName(Obj);

                //return Making list of JDP PRICING AND Vehicle info in same list
                List<JDPvehiclePricingInfoBydelarnameDTO> Listvehilcepricing = new List<JDPvehiclePricingInfoBydelarnameDTO>();

                //merging response data into one row
                foreach (var mergingdata in responseObj.Inventory.listOfVehicles)
                {
                    JDPvehiclePricingInfoBydelarnameDTO responsewithmergenewdto = new JDPvehiclePricingInfoBydelarnameDTO();

                    responsewithmergenewdto.Delearid = mergingdata.VehicleInfo.DealerID;
                    responsewithmergenewdto.Delearname = mergingdata.VehicleInfo.DealerName;
                    responsewithmergenewdto.Vin = mergingdata.VehicleInfo.VIN;
                    responsewithmergenewdto.StockNumber = mergingdata.VehicleInfo.StockNumber;
                    responsewithmergenewdto.IsNew = mergingdata.VehicleInfo.IsNew;
                    responsewithmergenewdto.InStockDate = mergingdata.VehicleInfo.InStockDate;
                    responsewithmergenewdto.Year = mergingdata.VehicleInfo.Year;
                    responsewithmergenewdto.Make = mergingdata.VehicleInfo.Make;
                    responsewithmergenewdto.Model = mergingdata.VehicleInfo.Model;
                    responsewithmergenewdto.ExteriorColor = mergingdata.VehicleInfo.ExteriorColor;
                    responsewithmergenewdto.Trim = mergingdata.VehicleInfo.Trim;
                    responsewithmergenewdto.BodyName = mergingdata.VehicleInfo.BodyName;
                    responsewithmergenewdto.BodyStyle = mergingdata.VehicleInfo.BodyStyle;
                    responsewithmergenewdto.ExtraPrice1 = mergingdata.Pricing.ExtraPrice1;
                    responsewithmergenewdto.ExtraPrice2 = mergingdata.Pricing.ExtraPrice2;
                    responsewithmergenewdto.ExtraPrice3 = mergingdata.Pricing.ExtraPrice3;
                    responsewithmergenewdto.Cost = mergingdata.Pricing.Cost;
                    responsewithmergenewdto.List = mergingdata.Pricing.List;
                    responsewithmergenewdto.Special = mergingdata.Pricing.Special;
                    responsewithmergenewdto.Adjustedwholeavg = 0;
                    responsewithmergenewdto.Adjustedwholerough = 0;
                    responsewithmergenewdto.Adjustedwholexclean = 0;
                    responsewithmergenewdto.Adjustedwholeclean = 0;
                    Listvehilcepricing.Add(responsewithmergenewdto);


                }
                // END OF MERGING

                var ConvertingResponsemodeltoJson = new JavaScriptSerializer().Serialize(responseObj);

                // for getting Response from API and this is used for insert.
                var modelListforupdate = JsonConvert.DeserializeObject<JDPStockVehiclesUpdateAPIResponseDTO>(ConvertingResponsemodeltoJson.ToString());
                //checking item exist or not in vehicle table

                int removeVin = 0;
                foreach (var isVehicleExist in responseObj.Inventory.listOfVehicles)
                {

                    var isVehicleExistdatacheck = _JDPcontext.JDPVehicleInfo.Where(i => i.VIN == isVehicleExist.VehicleInfo.VIN && i.DealerID == isVehicleExist.VehicleInfo.DealerID).ToList().Count;
                    if (isVehicleExistdatacheck > 0)
                    {

                        //removing index if item exist in table
                        modelListforupdate.Inventory.listOfVehicles.RemoveAt(removeVin);
                        removeVin--;
                    }
                    removeVin++;

                }

                //insert not exiting data 
                //initializing list object of table class
                List<JDPVehicleInfo> JDPVehicleInfolistLastList = new List<JDPVehicleInfo>();

                List<JDPExtendedDescriptions> JDPExtendedDescriptionsList_Final = new List<JDPExtendedDescriptions>();

                List<JDPListOfPhotos> JDPListOfPhotosList_Final = new List<JDPListOfPhotos>();

                List<JDPVehicleComments> JDPVehicleCommentsList_Final = new List<JDPVehicleComments>();

                List<JDPPremiumOptions> JDPPremiumOptionsList_Final = new List<JDPPremiumOptions>();

                List<JDPSubOptions> JDPSubOptionsList_Final = new List<JDPSubOptions>();

                List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList_Final = new List<JDPListOfAppliedOffers>();

                List<JDPListOfOptions> JDPListOfOptions_Final = new List<JDPListOfOptions>();

                List<JDPPricing> JDPPricingLists_Final = new List<JDPPricing>();

                foreach (var responsedata in modelListforupdate.Inventory.listOfVehicles)
                {
                    //initializing list object of table class
                    List<JDPVehicleInfo> JDPVehicleInfolist = new List<JDPVehicleInfo>();

                    List<JDPExtendedDescriptions> JDPExtendedDescriptionsList = new List<JDPExtendedDescriptions>();

                    List<JDPListOfPhotos> JDPListOfPhotosList = new List<JDPListOfPhotos>();

                    List<JDPVehicleComments> JDPVehicleCommentsList = new List<JDPVehicleComments>();

                    List<JDPPremiumOptions> JDPPremiumOptionsList = new List<JDPPremiumOptions>();

                    List<JDPSubOptions> JDPSubOptionsList = new List<JDPSubOptions>();

                    List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList = new List<JDPListOfAppliedOffers>();

                    List<JDPListOfOptions> JDPListOfOptions = new List<JDPListOfOptions>();

                    List<JDPPricing> JDPPricingLists = new List<JDPPricing>();

                    // maping response into model
                    JDPVehicleInfo objectvechicleInfo = InsertJDPVehicleInfoDealerWise(responsedata.VehicleInfo);
                    JDPVehicleInfolist.Add(objectvechicleInfo);


                    // function for insert Extended Descriptions
                    JDPExtendedDescriptions JDPExtendedDescriptionsObj = InsertJDPExtendedDescriptionsDealerWise(responsedata.VehicleInfo);
                    JDPExtendedDescriptionsList.Add(JDPExtendedDescriptionsObj);

                    InsertJDPListOfPhotosDealerWise(JDPListOfPhotosList, responsedata);

                    JDPVehicleComments objectJDPVehicleCommentsInfo = InsertJDPVehicleCommentsDealerWise(responsedata.VehicleInfo);
                    JDPVehicleCommentsList.Add(objectJDPVehicleCommentsInfo);

                    InsertJDPPremiumOptionsDealerWise(JDPPremiumOptionsList, responsedata);

                    InsertJDPSubOptionsDealerWise(JDPSubOptionsList, responsedata);

                    InsertJDPListOfAppliedOffersDealerWise(JDPListOfAppliedOffersList, responsedata);

                    InsertJDPListOfOptionsDealerWise(JDPListOfOptions, responsedata);

                    JDPPricing objectJDPPricing = InsertJDPPricingDealerWise(responsedata);
                    JDPPricingLists.Add(objectJDPPricing);

                    //inserting data into vehicleid and get primary id after insertion
                    await _jDPVehicleInfo.JDP_InsertMultiAsync(JDPVehicleInfolist);   //saving vehicle information

                    //assign vehicleinfo id into their child table 
                    JDPExtendedDescriptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPListOfPhotosList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPVehicleCommentsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPPremiumOptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPSubOptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPListOfAppliedOffersList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPListOfOptions.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                    JDPPricingLists.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);

                    //and adding list value of each page with final list for bulk insertion , i used Z.EntityFramework.Extensions.EFCore for bulk insertion and used in repository
                    JDPExtendedDescriptionsList_Final.AddRange(JDPExtendedDescriptionsList);
                    JDPListOfPhotosList_Final.AddRange(JDPListOfPhotosList);
                    JDPVehicleCommentsList_Final.AddRange(JDPVehicleCommentsList);
                    JDPPremiumOptionsList_Final.AddRange(JDPPremiumOptionsList);
                    JDPSubOptionsList_Final.AddRange(JDPSubOptionsList);
                    JDPListOfAppliedOffersList_Final.AddRange(JDPListOfAppliedOffersList);
                    JDPListOfOptions_Final.AddRange(JDPListOfOptions);
                    JDPPricingLists_Final.AddRange(JDPPricingLists);


                }

                //inserting vehicle info id into remaing table using repository pattern
                await _jDPExtendedDescriptions.JDP_InsertMultiAsync(JDPExtendedDescriptionsList_Final);   //saving JDPExtendedDescriptionsList
                await _jDPListOfPhotos.JDP_InsertMultiAsync(JDPListOfPhotosList_Final);   //saving JDPListOfPhotosList
                await _jDPVehicleComments.JDP_InsertMultiAsync(JDPVehicleCommentsList_Final); //saving JDP Comments
                await _jDPPremiumOptions.JDP_InsertMultiAsync(JDPPremiumOptionsList_Final); //saving JDP Comments
                await _jDPSubOptions.JDP_InsertMultiAsync(JDPSubOptionsList_Final); //saving JDP Sub Options
                await _JDPListOfAppliedOffers.JDP_InsertMultiAsync(JDPListOfAppliedOffersList_Final); //saving List Of Applied Offers
                await _JDPListOfOptions.JDP_InsertMultiAsync(JDPListOfOptions_Final); //saving JDP List Of Options
                await _jDPPricing.JDP_InsertMultiAsync(JDPPricingLists_Final); //saving JDP List Of Pricing


                //this code is for update data from api response
                if (responseObj != null)
                {
                    // For Updating VehicleStatus column in JDPVehicleInfo table
                    var MainXML = new XElement("DocumentElement");
                    foreach (var getdata in responseObj.Inventory.listOfVehicles)
                    {
                        var datacontent =
                                        new XElement("JDPVehicleInfo",
                                        new XElement("VIN", getdata.VehicleInfo.VIN),
                                        new XElement("VehicleStatus", getdata.VehicleInfo.VehicleStatus),
                                        new XElement("VehicleID", getdata.VehicleInfo.VehicleID),
                                        new XElement("DealerID", getdata.VehicleInfo.DealerID),
                                        new XElement("Cost", getdata.Pricing.Cost),
                                        new XElement("Special", getdata.Pricing.Special)

                                        );
                        MainXML.Add(datacontent);

                    }
                    if (MainXML.ToString() == "<DocumentElements/>")
                    {

                    }
                    else
                    {
                        _IICCBatchLogicBAL.UpdateVehiclesStatusIndatabase(MainXML.ToString());
                    }

                }

                // For Calling CBB API By VIN to Get 
                List<CBBPricing> JDPCBBPricings = new List<CBBPricing>();
                List<CBBPricing> CBBPricingLastList = new List<CBBPricing>();


                List<CBBPricing> objectCBBPricingCheck = new List<CBBPricing>();

                // for getting new Vehicles came from JDPower API but not in AithrX database - First time Entry to AithrX DB
                var VinNumberListForCBBAPIValues = _IICCBatchLogicBAL.GetVINforCBBAPIValues();
                int VINCBBCount = 0;
                int AcutaCBBCount = 0;
                if (VinNumberListForCBBAPIValues.Count > 0)
                {
                    // loop for CBB API call
                    foreach (var vindata in VinNumberListForCBBAPIValues)
                    {
                        // FOR CBB API CALL to get Adjustedwholeavg ,Adjustedwholerough,Adjustedwholexclean
                        IICCBatchApiDTO values = new IICCBatchApiDTO();
                        values.vin = vindata.VIN.ToString();
                        values.Mileage = vindata.Mileage;
                        // var CbbValuesList = _IICCBatchLogicBAL.GetVehiclesCBBAPIValueList(values);
                        var CbbValuesList = _IICCBatchLogicBAL.GetVehiclesCBBAPIByMileageValueList(values);

                        VINCBBCount = VINCBBCount + 1;
                       
                        // FOR MAPPING TO CBB PRICE
                        if (CbbValuesList.used_vehicles.used_vehicle_list.Count > 0)
                        {
                            AcutaCBBCount = AcutaCBBCount + 1;
                            var Isexitinvehiclelist = Listvehilcepricing.Where(i => i.Vin == vindata.VIN).FirstOrDefault();
                            if (Isexitinvehiclelist != null)
                            {
                                Isexitinvehiclelist.Adjustedwholeavg = Convert.ToInt32(CbbValuesList.used_vehicles.used_vehicle_list[0].adjusted_whole_avg);
                                Isexitinvehiclelist.Adjustedwholerough = Convert.ToInt32(CbbValuesList.used_vehicles.used_vehicle_list[0].adjusted_whole_rough);
                                Isexitinvehiclelist.Adjustedwholexclean = Convert.ToInt32(CbbValuesList.used_vehicles.used_vehicle_list[0].adjusted_whole_xclean);
                                Isexitinvehiclelist.Adjustedwholeclean = Convert.ToInt32(CbbValuesList.used_vehicles.used_vehicle_list[0].adjusted_whole_clean);
                            }

                           // objectCBBPricing = InsertCBBPricing(CbbValuesList, vindata.VIN, vindata.VehicleID, vindata.DealerID, vindata.id);

                         
                            objectCBBPricingCheck = InsertCBBPricing(CbbValuesList, vindata.VIN, vindata.VehicleID, vindata.DealerID, vindata.id);
                            // JDPCBBPricings.Add(objectCBBPricing);

                            CBBPricingLastList.AddRange(objectCBBPricingCheck);
                            await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList);

                            CBBPricingLastList.Clear();
                            objectCBBPricingCheck.Clear();

                            // CBBPricing objectCBBPricing = InsertCBBPricing(CbbValuesList, vindata.VIN, vindata.VehicleID, vindata.DealerID, vindata.id);
                            // JDPCBBPricings.Add(objectCBBPricing);
                        }

                    }

                   
                }


                //// FOR INSERT BATCH
                //CBBPricingLastList.AddRange(JDPCBBPricings);
                //await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList); //saving JDP CBB Pricing

                //CBBPricingLastList.AddRange(objectCBBPricing);
                //await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList); //saving JDP CBB Pricing

                // FOR Storing Count of CBB API Pull
                var CountCBBlist = _IICCBatchLogicBAL.InsertCBBPricingAPIDetail(AcutaCBBCount, 
                    Convert.ToString(APICallFunctions.GetVehiclesByDealerNames) , VINCBBCount);

                // get email template and Replace count
                string emailTemplateCheck = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "EmailTemplate.txt"));
                emailTemplateCheck = emailTemplateCheck.Replace("VINCount", VINCBBCount.ToString());
                emailTemplateCheck = emailTemplateCheck.Replace("CBBCount", AcutaCBBCount.ToString());


                // For getting CBB API Pull detail Dealer wise.
                var CBBPullAPIDetail = _IICCBatchLogicBAL.GetJDPCBBPricingDetailForEmailByDealerNames();
                string CbbData = "";

                if (CBBPullAPIDetail.Count > 0)
                {
                    for (var i = 0; i < CBBPullAPIDetail.Count; i++)
                    {
                        CbbData +="" + CBBPullAPIDetail[i].DealerName + ":    " + CBBPullAPIDetail[i].CBBPullCount + "<br/><br/>";
                    }

                }
                emailTemplateCheck = emailTemplateCheck.Replace("CBBPullData", CbbData.ToString());
                
                EmailHelper emailHelper = new EmailHelper(_appSettings, _IICCBatchLogicBAL);
                CommonHelper commonHelper = new CommonHelper(_appSettings, _IICCBatchLogicBAL);

                // For getting email and Send email to User and Admin
                var adminEmail = commonHelper.GetDescriptionOfEnum(EmailSend.Admin);
                var UserEmail = commonHelper.GetDescriptionOfEnum(EmailSend.User);

               // bool emailAdminResponse = emailHelper.SendCBBEmail(adminEmail, emailTemplateCheck);
                bool emailResponse = emailHelper.SendCBBEmail(UserEmail, emailTemplateCheck);

                

                // For inserting Dealers from JDP Vehicle table to JDP Dealer info table and if already exist it
                // will not insert dealers again
                var dealerlist = await _IICCBatchLogicBAL.InsertDealersFromVehicleInfoDealerWise("2", DealerId);


                if (Listvehilcepricing == null)
                    return NotFound();
                return Ok(Listvehilcepricing.Count);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// This method is calling to get all Vehicles list for all page
        /// </summary>
        /// <returns></returns>
        /// 
       // [Authorize]
        [HttpPost]
        [Route("GetAllVehiclesInformationAllPages")]
        public async Task<IActionResult> GetAllVehiclesInformationAllPages([FromBody] IICCBatchApiDTO Obj,string CallBy)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //here we calling GetVehiclesDetailsForAllPages() method from ICCbatchlogic service
                var responseObj = await _IICCBatchLogicBAL.GetVehiclesDetailsForAllPages(Obj);   //getting response

                // for updating the existing status to false for Vehicle
                // var updatestatus = _IICCBatchLogicBAL.UpdateVehicleStatus();

                //initializing list object of table class

                List<JDPVehicleInfo> JDPVehicleInfolistLastList = new List<JDPVehicleInfo>();

                List<JDPExtendedDescriptions> JDPExtendedDescriptionsList_Final = new List<JDPExtendedDescriptions>();

                List<JDPListOfPhotos> JDPListOfPhotosList_Final = new List<JDPListOfPhotos>();

                List<JDPVehicleComments> JDPVehicleCommentsList_Final = new List<JDPVehicleComments>();

                List<JDPPremiumOptions> JDPPremiumOptionsList_Final = new List<JDPPremiumOptions>();

                List<JDPSubOptions> JDPSubOptionsList_Final = new List<JDPSubOptions>();

                List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList_Final = new List<JDPListOfAppliedOffers>();

                List<JDPListOfOptions> JDPListOfOptions_Final = new List<JDPListOfOptions>();

                List<JDPPricing> JDPPricingLists_Final = new List<JDPPricing>();


                //here we applying for each page response data
                foreach (var responsedata in responseObj)
                {                 
                    foreach (var response in responsedata.Data)
                    {


                        //initializing list object of table class
                        List<JDPVehicleInfo> JDPVehicleInfolist = new List<JDPVehicleInfo>();

                        List<JDPExtendedDescriptions> JDPExtendedDescriptionsList = new List<JDPExtendedDescriptions>();

                        List<JDPListOfPhotos> JDPListOfPhotosList = new List<JDPListOfPhotos>();

                        List<JDPVehicleComments> JDPVehicleCommentsList = new List<JDPVehicleComments>();

                        List<JDPPremiumOptions> JDPPremiumOptionsList = new List<JDPPremiumOptions>();

                        List<JDPSubOptions> JDPSubOptionsList = new List<JDPSubOptions>();

                        List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList = new List<JDPListOfAppliedOffers>();

                        List<JDPListOfOptions> JDPListOfOptions = new List<JDPListOfOptions>();

                        List<JDPPricing> JDPPricingLists = new List<JDPPricing>();


                        // maping response into model
                        JDPVehicleInfo objectvechicleInfo = InsertJDPVehicleInfo(response , CallBy);
                        JDPVehicleInfolist.Add(objectvechicleInfo);

                        // function for insert Extended Descriptions
                        JDPExtendedDescriptions JDPExtendedDescriptionsObj = InsertJDPExtendedDescriptions(response);
                        JDPExtendedDescriptionsList.Add(JDPExtendedDescriptionsObj);

                        InsertJDPListOfPhotos(JDPListOfPhotosList, response);
                        JDPVehicleComments objectJDPVehicleCommentsInfo = InsertJDPVehicleComments(response);
                        JDPVehicleCommentsList.Add(objectJDPVehicleCommentsInfo);

                        InsertJDPPremiumOptions(JDPPremiumOptionsList, response);

                        InsertJDPSubOptions(JDPSubOptionsList, response);

                        InsertJDPListOfAppliedOffers(JDPListOfAppliedOffersList, response);

                        InsertJDPListOfOptions(JDPListOfOptions, response);

                        JDPPricing objectJDPPricing = InsertJDPPricing(response);
                        JDPPricingLists.Add(objectJDPPricing);


                        ////inserting data into vehicleid and get primary id after insertion
                        await _jDPVehicleInfo.JDP_InsertMultiAsync(JDPVehicleInfolist);   //saving vehicle information

                        //assign vehicleinfo id into their child table 
                        JDPExtendedDescriptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPListOfPhotosList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPVehicleCommentsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPPremiumOptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPSubOptionsList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPListOfAppliedOffersList.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPListOfOptions.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);
                        JDPPricingLists.ToList().ForEach(i => i.JDPVehicleInfoId = JDPVehicleInfolist.FirstOrDefault().id);


                        //and adding list value of each page with final list for bulk insertion , i used Z.EntityFramework.Extensions.EFCore for bulk insertion and used in repository
                        JDPExtendedDescriptionsList_Final.AddRange(JDPExtendedDescriptionsList);
                        JDPListOfPhotosList_Final.AddRange(JDPListOfPhotosList);
                        JDPVehicleCommentsList_Final.AddRange(JDPVehicleCommentsList);
                        JDPPremiumOptionsList_Final.AddRange(JDPPremiumOptionsList);
                        JDPSubOptionsList_Final.AddRange(JDPSubOptionsList);
                        JDPListOfAppliedOffersList_Final.AddRange(JDPListOfAppliedOffersList);
                        JDPListOfOptions_Final.AddRange(JDPListOfOptions);
                        JDPPricingLists_Final.AddRange(JDPPricingLists);
                    }                  
                }



                ////inserting vehicle info id into remaing table using repository pattern
                await _jDPExtendedDescriptions.JDP_InsertMultiAsync(JDPExtendedDescriptionsList_Final);   //saving JDPExtendedDescriptionsList
                await _jDPListOfPhotos.JDP_InsertMultiAsync(JDPListOfPhotosList_Final);   //saving JDPListOfPhotosList
                await _jDPVehicleComments.JDP_InsertMultiAsync(JDPVehicleCommentsList_Final); //saving JDP Comments
                await _jDPPremiumOptions.JDP_InsertMultiAsync(JDPPremiumOptionsList_Final); //saving JDP Comments
                await _jDPSubOptions.JDP_InsertMultiAsync(JDPSubOptionsList_Final); //saving JDP Sub Options
                await _JDPListOfAppliedOffers.JDP_InsertMultiAsync(JDPListOfAppliedOffersList_Final); //saving List Of Applied Offers
                await _JDPListOfOptions.JDP_InsertMultiAsync(JDPListOfOptions_Final); //saving JDP List Of Options
                await _jDPPricing.JDP_InsertMultiAsync(JDPPricingLists_Final); //saving JDP List Of Pricing

                // For Calling CBB API By VIN to Get 
                List<CBBPricing> JDPCBBPricings = new List<CBBPricing>();
                List<CBBPricing> CBBPricingLastList = new List<CBBPricing>();

                List<CBBPricing> objectCBBPricingCheck = new List<CBBPricing>();

                // for getting new Vehicles came from JDPower API but not in AithrX database - First time Entry to AithrX DB
                var VinNumberListForCBBAPIValues = _IICCBatchLogicBAL.GetVINforCBBAPIValues();
                int VINCBBCount = 0;
                int AcutaCBBCount = 0;
                if (VinNumberListForCBBAPIValues.Count > 0)
                {
                    // loop for CBB API call
                    foreach (var vindata in VinNumberListForCBBAPIValues)
                    {
                        // FOR CBB API CALL to get Adjustedwholeavg ,Adjustedwholerough,Adjustedwholexclean
                        IICCBatchApiDTO values = new IICCBatchApiDTO();
                        values.vin = vindata.VIN.ToString();
                        values.Mileage = vindata.Mileage;
                        //  var CbbValuesList = _IICCBatchLogicBAL.GetVehiclesCBBAPIValueList(values);
                        var CbbValuesList = _IICCBatchLogicBAL.GetVehiclesCBBAPIByMileageValueList(values);

                        VINCBBCount = VINCBBCount + 1;
                        // FOR MAPPING TO CBB PRICE
                        if (CbbValuesList.used_vehicles.used_vehicle_list.Count > 0)
                        {
                            AcutaCBBCount = AcutaCBBCount + 1;
                            objectCBBPricingCheck = InsertCBBPricing(CbbValuesList, vindata.VIN, vindata.VehicleID, vindata.DealerID, vindata.id);
                            // JDPCBBPricings.Add(objectCBBPricing);

                            CBBPricingLastList.AddRange(objectCBBPricingCheck);
                            await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList);

                            CBBPricingLastList.Clear();
                            objectCBBPricingCheck.Clear();

                        }

                    }

                }

                //CBBPricingLastList.AddRange(objectCBBPricing);
                //await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList); //saving JDP CBB Pricing

                // For checking how many time CBB API call
                var CountCBBlist = _IICCBatchLogicBAL.InsertCBBPricingAPIDetail(AcutaCBBCount, Convert.ToString(APICallFunctions.GetAllVehiclesInformationAllPages),
                    VINCBBCount);

                // get email template and Replace count
                string emailTemplateCheck = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "EmailTemplate.txt"));
                emailTemplateCheck = emailTemplateCheck.Replace("VINCount", VINCBBCount.ToString());
                emailTemplateCheck = emailTemplateCheck.Replace("CBBCount", AcutaCBBCount.ToString());


                // For getting CBB API Pull detail Dealer wise.
                var CBBPullAPIDetail = _IICCBatchLogicBAL.GetJDPCBBPricingDetailForEmailByDealerNames();
                string CbbData = "";

                if (CBBPullAPIDetail.Count > 0)
                {
                    for (var i = 0; i < CBBPullAPIDetail.Count; i++)
                    {
                        CbbData += "" + CBBPullAPIDetail[i].DealerName + ":    " + CBBPullAPIDetail[i].CBBPullCount + "<br/><br/>";
                    }

                }
                emailTemplateCheck = emailTemplateCheck.Replace("CBBPullData", CbbData.ToString());
                EmailHelper emailHelper = new EmailHelper(_appSettings, _IICCBatchLogicBAL);
                CommonHelper commonHelper = new CommonHelper(_appSettings, _IICCBatchLogicBAL);

                // For getting email and Send email to User and Admin
                var adminEmail = commonHelper.GetDescriptionOfEnum(EmailSend.Admin);
                var UserEmail = commonHelper.GetDescriptionOfEnum(EmailSend.User);

                bool emailAdminResponse = emailHelper.SendCBBEmail(adminEmail, emailTemplateCheck);
                bool emailResponse = emailHelper.SendCBBEmail(UserEmail, emailTemplateCheck);

                // FOR INSERT BATCH
                //CBBPricingLastList.AddRange(JDPCBBPricings);
                //await _cBBPricing.JDP_InsertMultiAsync(CBBPricingLastList); //saving JDP CBB Pricing

                // For inserting Dealers from JDP Vehicle table to JDP Dealer info table and if already exist it
                // will not insert dealers again
                  var dealerlist = await _IICCBatchLogicBAL.InsertDealersFromVehicleInfo("1" ,"0");
                  var dealerlistCheck = await _IICCBatchLogicBAL.SyncJDPVehicleInfo("1", "0");
                if (responseObj == null)
                    return NotFound();
                return Ok("Success");
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
      
        #region Insert JDP Vehicle Details dealer wise

        private static void InsertJDPListOfOptionsDealerWise(List<JDPListOfOptions> JDPListOfOptionsList, Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {

            if (response.ListOfOptions != null)
            {
                foreach (var listOfOptionlist in response.ListOfOptions)
                {

                    JDPListOfOptions JDPListOfOptionsObj = new JDPListOfOptions();
                    JDPListOfOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfOptionsObj.DealerID = response.VehicleInfo.DealerID;
                    JDPListOfOptionsObj.VehicleID = response.VehicleInfo.VehicleID;
                    JDPListOfOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                  //  JDPListOfOptionsObj.Code = TrimStringValueToLimit(listOfOptionlist.Code, 100);
                    JDPListOfOptionsObj.Type = TrimStringValueToLimit(listOfOptionlist.Type, 100);
                    JDPListOfOptionsObj.Description = TrimStringValueToLimit(listOfOptionlist.Description.ToString(), 200);
                    JDPListOfOptionsObj.Header = TrimStringValueToLimit(listOfOptionlist.Header, 100);
                    JDPListOfOptionsObj.Msrp = listOfOptionlist.Msrp;
                   // JDPListOfOptionsObj.ImageUrl = TrimStringValueToLimit(listOfOptionlist.ImageUrl, 200);
                    JDPListOfOptionsObj.Order = 0;
                    JDPListOfOptionsObj.IsActive = true;
                    JDPListOfOptionsObj.CreatedDate = DateTime.Now;
                    JDPListOfOptionsObj.CreatedBy = 1;
                    JDPListOfOptionsList.Add(JDPListOfOptionsObj);
                }
            }

        }
        private static JDPVehicleInfo InsertJDPVehicleInfoDealerWise(Airthwholesale.Bal.DTO.StockVehiclesUpdate.VehicleInfo response)
        {
            JDPVehicleInfo objectvechicleinfo = new JDPVehicleInfo();
            objectvechicleinfo.VehicleID = response.VehicleID;
            objectvechicleinfo.DealerID = response.DealerID;
            objectvechicleinfo.DealerName = TrimStringValueToLimit(response.DealerName, 100);
            objectvechicleinfo.IsNew = response.IsNew;
            objectvechicleinfo.VIN = TrimStringValueToLimit(response.VIN, 17);
            objectvechicleinfo.StockNumber = TrimStringValueToLimit(response.StockNumber, 100);
            objectvechicleinfo.IsCertified = response.IsCertified;
            objectvechicleinfo.Year = (response.Year == 0 ? 0000 : response.Year);
            objectvechicleinfo.Make = TrimStringValueToLimit(response.Make, 100);
            objectvechicleinfo.Model = TrimStringValueToLimit(response.Model, 100);
            objectvechicleinfo.ModelCode = response.ModelCode;
            objectvechicleinfo.Trim = TrimStringValueToLimit(response.Trim, 100);
            objectvechicleinfo.BodyName = TrimStringValueToLimit((response.BodyName == null ? "" : response.BodyName.ToString()), 100);
            objectvechicleinfo.BodyStyle = TrimStringValueToLimit(response.BodyStyle.ToString(), 100);
            objectvechicleinfo.CityMPG = response.CityMPG;
            objectvechicleinfo.HwyMPG = response.HwyMPG;
            objectvechicleinfo.DaysInSotck = response.DaysInSotck;
            objectvechicleinfo.ValueSource = TrimStringValueToLimit(response.ValueSource, 100);
            objectvechicleinfo.ExteriorColor = TrimStringValueToLimit(response.ExteriorColor, 100);
            objectvechicleinfo.InteriorColor = TrimStringValueToLimit(response.InteriorColor, 100);
            objectvechicleinfo.InteriorMaterial = TrimStringValueToLimit(response.InteriorMaterial, 100);
            objectvechicleinfo.Engine = TrimStringValueToLimit(response.Engine, 100);

            objectvechicleinfo.Transmission = TrimStringValueToLimit(response.Transmission, 100);
            objectvechicleinfo.TransmissionSpeed = TrimStringValueToLimit(response.TransmissionSpeed, 100);

            objectvechicleinfo.InStockDate = response.InStockDate;
            objectvechicleinfo.LastModifiedDate = response.LastModifiedDate;
            objectvechicleinfo.IsSpecial = (response.IsSpecial == null ? false : Convert.ToBoolean(response.IsSpecial));
            objectvechicleinfo.BodyType = TrimStringValueToLimit(response.BodyType, 100);
            objectvechicleinfo.Locked = response.Locked;
            objectvechicleinfo.VehicleStatus = TrimStringValueToLimit(response.VehicleStatus, 100);
            objectvechicleinfo.DealerCertified1 = response.DealerCertified1;
            objectvechicleinfo.DealerCertified2 = response.DealerCertified2;
            objectvechicleinfo.GenericExteriorColor = TrimStringValueToLimit((response.GenericExteriorColor == null ? "" : response.GenericExteriorColor.ToString()), 100);
            objectvechicleinfo.VideoUrl = TrimStringValueToLimit(response.VideoUrl, 200);
            objectvechicleinfo.Drivetrain = TrimStringValueToLimit(response.Drivetrain, 100);
            objectvechicleinfo.Mileage = response.Mileage;
            objectvechicleinfo.Category1 = TrimStringValueToLimit((response.Category1 == null ? "" : response.Category1.ToString()), 100);
            objectvechicleinfo.Category2 = TrimStringValueToLimit((response.Category2 == null ? "" : response.Category2.ToString()), 100);
            objectvechicleinfo.Category3 = TrimStringValueToLimit((response.Category3 == null ? "" : response.Category3.ToString()), 100);
            objectvechicleinfo.Category4 = TrimStringValueToLimit((response.Category4 == null ? "" : response.Category4.ToString()), 100);
            objectvechicleinfo.Category5 = TrimStringValueToLimit((response.Category5 == null ? "" : response.Category5.ToString()), 100);
            objectvechicleinfo.Style = TrimStringValueToLimit(response.Style, 100);
            objectvechicleinfo.ChromeStyleID = response.ChromeStyleID;
            objectvechicleinfo.ZipCode = TrimStringValueToLimit(response.ZipCode, 100);
            objectvechicleinfo.ExportDealerID = TrimStringValueToLimit(response.ExportDealerID, 100);
            objectvechicleinfo.EngineFuelType = TrimStringValueToLimit(response.EngineFuelType, 100);
            objectvechicleinfo.ExteriorColorCode = TrimStringValueToLimit((response.ExteriorColorCode == null ? "" : response.ExteriorColorCode.ToString()), 100);
            objectvechicleinfo.ExteriorGenericColorDescription = TrimStringValueToLimit((response.ExteriorGenericColorDescription == null ? "" : response.ExteriorGenericColorDescription.ToString()), 100);
            objectvechicleinfo.IsActive = true;
            objectvechicleinfo.CreatedDate = DateTime.Now;
            objectvechicleinfo.CreatedBy = 99;
            objectvechicleinfo.IsInternalSynch = false;
            objectvechicleinfo.SynchedBy = 10;
            objectvechicleinfo.InternalID = 1;
            return objectvechicleinfo;
        }

        private static JDPExtendedDescriptions InsertJDPExtendedDescriptionsDealerWise(Airthwholesale.Bal.DTO.StockVehiclesUpdate.VehicleInfo response)
        {
            JDPExtendedDescriptions JDPExtendedDescriptionsObj = new JDPExtendedDescriptions();

            JDPExtendedDescriptionsObj.JDPVehicleInfoId = response.VehicleID;
            JDPExtendedDescriptionsObj.DealerID = response.DealerID;
            JDPExtendedDescriptionsObj.VehicleID = response.VehicleID;
            JDPExtendedDescriptionsObj.ExtendedVehicleName = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedVehicleName, 100);
            JDPExtendedDescriptionsObj.ExtendedBodyStyle = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedBodyStyle, 100);
            JDPExtendedDescriptionsObj.ExtendedDrivetrain = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedDrivetrain, 100);
            JDPExtendedDescriptionsObj.ExtendedDisplacement = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedDisplacement, 100);
            JDPExtendedDescriptionsObj.ExtendedEngineType = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedEngineType, 100);
            JDPExtendedDescriptionsObj.ExtendedFuelSystem = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedFuelSystem, 100);
            JDPExtendedDescriptionsObj.ExtendedTransmissionDescripCont = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedTransmissionDescripCont, 100);
            JDPExtendedDescriptionsObj.ExtendedTransType = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedTransType, 100);
            JDPExtendedDescriptionsObj.ExtendedBestmakename = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedBestmakename, 100);
            JDPExtendedDescriptionsObj.ExtendedBestmodelname = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedBestmodelname, 100);
            JDPExtendedDescriptionsObj.ExtendedBeststylename = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedBeststylename, 100);
            JDPExtendedDescriptionsObj.ExtendedBesttrimname = TrimStringValueToLimit(response.ExtendedDescriptions.ExtendedBesttrimname, 100);
            JDPExtendedDescriptionsObj.IsActive = true;
            JDPExtendedDescriptionsObj.CreatedDate = DateTime.Now;
            JDPExtendedDescriptionsObj.CreatedBy = 1;
            return JDPExtendedDescriptionsObj;
        }

        private static void InsertJDPListOfPhotosDealerWise(List<JDPListOfPhotos> JDPListOfPhotosList, Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {

            if (response != null)
            {
                foreach (var photolist in response.ListOfPhotos)
                {

                    JDPListOfPhotos VehiclePhotosListObj = new JDPListOfPhotos();
                    VehiclePhotosListObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    VehiclePhotosListObj.DealerID = response.VehicleInfo.DealerID;
                    VehiclePhotosListObj.VehicleID = response.VehicleInfo.VehicleID;
                    VehiclePhotosListObj.VehiclePhotoID = TrimStringValueToLimit(photolist.VehiclePhotoID, 100);
                    VehiclePhotosListObj.PhotoUrl = TrimStringValueToLimit(photolist.PhotoUrl, 200);
                    VehiclePhotosListObj.Order = photolist.Order;
                    VehiclePhotosListObj.PhotoTimestamp = photolist.PhotoTimestamp;
                    VehiclePhotosListObj.IsActive = true;
                    VehiclePhotosListObj.CreatedDate = DateTime.Now;
                    VehiclePhotosListObj.CreatedBy = 1;
                    JDPListOfPhotosList.Add(VehiclePhotosListObj);
                }
            }
        }

        private static JDPVehicleComments InsertJDPVehicleCommentsDealerWise(Airthwholesale.Bal.DTO.StockVehiclesUpdate.VehicleInfo response)
        {
            JDPVehicleComments objecVehicleComments = new JDPVehicleComments();

            objecVehicleComments.VehicleID = response.VehicleID;
            objecVehicleComments.DealerID = response.DealerID;
            objecVehicleComments.Comments = TrimStringValueToLimit(response.Comments, 999);
            objecVehicleComments.Comments2 = TrimStringValueToLimit(response.Comments2, 999);
            objecVehicleComments.Comments3 = TrimStringValueToLimit(response.Comments3, 999);
            objecVehicleComments.Comments4 = TrimStringValueToLimit(response.Comments4, 999);
            objecVehicleComments.Comments5 = TrimStringValueToLimit(response.Comments5, 999);
            objecVehicleComments.CreatedBy = 1;
            objecVehicleComments.IsActive = true;
            objecVehicleComments.CreatedDate = DateTime.Now;
            return objecVehicleComments;
        }

        private static void InsertJDPPremiumOptionsDealerWise(List<JDPPremiumOptions> JDPPremiumOptionsList, Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {

            if (response.PremiumOptions != null)
            {
                foreach (var PremiumOptionsObjlist in response.PremiumOptions)
                {

                    JDPPremiumOptions PremiumOptionsObj = new JDPPremiumOptions();
                    PremiumOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    PremiumOptionsObj.DealerID = response.VehicleInfo.DealerID;
                    PremiumOptionsObj.VehicleID = response.VehicleInfo.VehicleID;

                    // PremiumOptionsObj.Order = PremiumOptionsObjlist.Order;

                    PremiumOptionsObj.Code = TrimStringValueToLimit(PremiumOptionsObjlist.Code, 100);
                    PremiumOptionsObj.Name = TrimStringValueToLimit(PremiumOptionsObjlist.Name, 100);
                    PremiumOptionsObj.Order = 1;

                    PremiumOptionsObj.IsActive = true;
                    PremiumOptionsObj.CreatedBy = 1;
                    PremiumOptionsObj.CreatedDate = DateTime.Now;
                    JDPPremiumOptionsList.Add(PremiumOptionsObj);
                }
            }
        }

        private static void InsertJDPListOfAppliedOffersDealerWise(List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList, Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {
            if (response.ListOfAppliedOffers != null)
            {

                foreach (var appliedOfferslist in response.ListOfAppliedOffers)
                {

                    JDPListOfAppliedOffers JDPListOfAppliedOffersObj = new JDPListOfAppliedOffers();
                    JDPListOfAppliedOffersObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfAppliedOffersObj.DealerID = response.VehicleInfo.DealerID;
                    JDPListOfAppliedOffersObj.VehicleID = response.VehicleInfo.VehicleID;
                    JDPListOfAppliedOffersObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                   // JDPListOfAppliedOffersObj.Price = TrimStringValueToLimit(appliedOfferslist.Price, 100);
                 //   JDPListOfAppliedOffersObj.Amount = Convert.ToDecimal(appliedOfferslist.Amount);
                   // JDPListOfAppliedOffersObj.OfferName = TrimStringValueToLimit(appliedOfferslist.OfferName.ToString(), 100);
                  //  JDPListOfAppliedOffersObj.OfferDescription = TrimStringValueToLimit(appliedOfferslist.OfferDescription, 200);
                   // JDPListOfAppliedOffersObj.OfferStartDate = appliedOfferslist.OfferStartDate;
                   // JDPListOfAppliedOffersObj.OfferEndDate = appliedOfferslist.OfferEndDate;
                    JDPListOfAppliedOffersObj.Order = 0;
                    JDPListOfAppliedOffersObj.IsActive = true;
                    JDPListOfAppliedOffersObj.CreatedDate = DateTime.Now;
                    JDPListOfAppliedOffersObj.CreatedBy = 1;
                    JDPListOfAppliedOffersList.Add(JDPListOfAppliedOffersObj);
                }
            }
        }
        private static JDPPricing InsertJDPPricingDealerWise(Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {
            JDPPricing objectJDPPricing = new JDPPricing();
            objectJDPPricing.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
            objectJDPPricing.VehicleID = response.VehicleInfo.VehicleID;
            objectJDPPricing.DealerID = response.VehicleInfo.DealerID;
            objectJDPPricing.Cost = response.Pricing.Cost;
            objectJDPPricing.ExtraPrice1 = response.Pricing.ExtraPrice1;
            objectJDPPricing.ExtraPrice2 = response.Pricing.ExtraPrice2;
            objectJDPPricing.ExtraPrice3 = response.Pricing.ExtraPrice3;
            objectJDPPricing.List = response.Pricing.List;
            objectJDPPricing.Special = response.Pricing.Special;
            objectJDPPricing.Order = 1;
            objectJDPPricing.IsActive = true;
            objectJDPPricing.CreatedDate = DateTime.Now;
            objectJDPPricing.CreatedBy = 1;
            return objectJDPPricing;
        }



        private static void InsertJDPSubOptionsDealerWise(List<JDPSubOptions> JDPSubOptionsList, Airthwholesale.Bal.DTO.StockVehiclesUpdate.ListOfVehicle response)
        {
            if (response.PremiumOptions != null)
            {

                foreach (var PremiumOptionsObjlist in response.PremiumOptions)
                {

                    if (PremiumOptionsObjlist.SubOptions != null)
                    {
                        foreach (var JDPSubOptionsListObj in PremiumOptionsObjlist.SubOptions)
                        {

                            JDPSubOptions JDPSubOptionsObj = new JDPSubOptions();
                            JDPSubOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                            JDPSubOptionsObj.DealerID = response.VehicleInfo.DealerID;
                            JDPSubOptionsObj.VehicleID = response.VehicleInfo.VehicleID;
                            JDPSubOptionsObj.Name = TrimStringValueToLimit(JDPSubOptionsListObj.Name, 100);
                            JDPSubOptionsObj.Msrp = Convert.ToDecimal(JDPSubOptionsListObj.Msrp);
                            JDPSubOptionsObj.Order = 0;

                            //JDPSubOptionsObj.Code = JDPSubOptionsObj.Code;
                            //JDPSubOptionsObj.IsChecked = JDPSubOptionsObj.IsChecked;
                            //JDPSubOptionsObj.OptionalOptionId = JDPSubOptionsObj.OptionalOptionId;
                            //JDPSubOptionsObj.packageID = JDPSubOptionsObj.packageID;
                            //JDPSubOptionsObj.packageParentID = JDPSubOptionsObj.packageParentID;

                            JDPSubOptionsObj.IsActive = true;
                            JDPSubOptionsObj.CreatedBy = 1;
                            JDPSubOptionsObj.CreatedDate = DateTime.Now;
                            JDPSubOptionsList.Add(JDPSubOptionsObj);
                        }
                    }
                }
            }
        }
        #endregion
        #region Insert JDP Vehicle Details

        //delaer wise function for insert vehicle info and Dealer wise





        /// <summary>
        /// Functions for trim columns values
        /// </summary>
        /// <returns></returns>
        /// 
        public static string TrimStringValueToLimit( string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                return value = string.Join("", value.Take(maxLength));
            }

            return value;
        }


        /// <summary>
        /// Insert Values in CBB price values
        /// </summary>
        /// <returns></returns>
        /// 
        private static List<CBBPricing> InsertCBBPricing(JDPCBBAPIResponseDTO response, string VIN , int VehicleID,int DealerID , long JDPVehicleInfoId)
        {
            

            List<CBBPricing> JDPCBBPricingsList = new List<CBBPricing>();

            foreach (var data in response.used_vehicles.used_vehicle_list)
            {
                CBBPricing objectcbbPriceinfo = new CBBPricing();
                objectcbbPriceinfo.JDPVehicleInfoId = JDPVehicleInfoId;
                objectcbbPriceinfo.VehicleID = VehicleID;
                objectcbbPriceinfo.DealerID = DealerID;
                objectcbbPriceinfo.VIN = VIN;
                objectcbbPriceinfo.Adjustedwholeavg = Convert.ToInt32(data.adjusted_whole_avg);
                objectcbbPriceinfo.Adjustedwholerough = Convert.ToInt32(data.adjusted_whole_rough);
                objectcbbPriceinfo.Adjustedwholexclean = Convert.ToInt32(data.adjusted_whole_xclean);
                objectcbbPriceinfo.Adjustedwholeclean = Convert.ToInt32(data.adjusted_whole_clean);

                // New columns changes

                objectcbbPriceinfo.Year = data.model_year;
                objectcbbPriceinfo.Model = data.model;
                objectcbbPriceinfo.Make = data.make;
                objectcbbPriceinfo.Series = data.series;
                objectcbbPriceinfo.Style = data.style;
                objectcbbPriceinfo.ClassName = data.class_name;
                objectcbbPriceinfo.Trim = data.series;

                // objectcbbPriceinfo.Adjustedwholeavg = Convert.ToInt32(response.used_vehicles.used_vehicle_list[0].adjusted_whole_avg);
                //objectcbbPriceinfo.Adjustedwholerough = Convert.ToInt32(response.used_vehicles.used_vehicle_list[0].adjusted_whole_rough);
                // objectcbbPriceinfo.Adjustedwholexclean = Convert.ToInt32(response.used_vehicles.used_vehicle_list[0].adjusted_whole_xclean);
                // objectcbbPriceinfo.Adjustedwholeclean = Convert.ToInt32(response.used_vehicles.used_vehicle_list[0].adjusted_whole_clean);
                objectcbbPriceinfo.Order = 0;
                objectcbbPriceinfo.IsActive = true;
                objectcbbPriceinfo.CreatedBy = 1;
                objectcbbPriceinfo.CreatedDate = DateTime.Now;
                JDPCBBPricingsList.Add(objectcbbPriceinfo);
            }
            return JDPCBBPricingsList;
        }

        /// <summary>
        /// Functions for Map resposne to  JDP List Of Options
        /// </summary>
        /// <returns></returns>
        /// 
        private static void InsertJDPListOfOptions(List<JDPListOfOptions> JDPListOfOptionsList, Datum response)
        {

            if (response.ListOfOption != null)
            {
                foreach (var listOfOptionlist in response.ListOfOption)
                {

                    JDPListOfOptions JDPListOfOptionsObj = new JDPListOfOptions();
                    JDPListOfOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfOptionsObj.DealerID = response.VehicleInfo.DealerID;
                    JDPListOfOptionsObj.VehicleID = response.VehicleInfo.VehicleID;
                    JDPListOfOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfOptionsObj.Code = TrimStringValueToLimit(listOfOptionlist.Code,100);
                    JDPListOfOptionsObj.Type = TrimStringValueToLimit(listOfOptionlist.Type,100);
                    JDPListOfOptionsObj.Description = TrimStringValueToLimit(listOfOptionlist.Description.ToString(),200);
                    JDPListOfOptionsObj.Header = TrimStringValueToLimit(listOfOptionlist.Header,100);
                    JDPListOfOptionsObj.Msrp = listOfOptionlist.Msrp;
                    JDPListOfOptionsObj.ImageUrl = TrimStringValueToLimit(listOfOptionlist.ImageUrl,200);
                    JDPListOfOptionsObj.Order = 0;
                    JDPListOfOptionsObj.IsActive = true;
                    JDPListOfOptionsObj.CreatedDate = DateTime.Now;
                    JDPListOfOptionsObj.CreatedBy = 1;
                    JDPListOfOptionsList.Add(JDPListOfOptionsObj);
                }
            }
 
        }
        /// <summary>
        /// Functions for Map resposne to  JDP List Of Applied Offers
        /// </summary>
        /// <returns></returns>
        /// 
        private static void InsertJDPListOfAppliedOffers(List<JDPListOfAppliedOffers> JDPListOfAppliedOffersList, Datum response)
        {
            if (response.ListOfAppliedOffers != null)
            {

                foreach (var appliedOfferslist in response.ListOfAppliedOffers)
                {

                    JDPListOfAppliedOffers JDPListOfAppliedOffersObj = new JDPListOfAppliedOffers();
                    JDPListOfAppliedOffersObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfAppliedOffersObj.DealerID = response.VehicleInfo.DealerID;
                    JDPListOfAppliedOffersObj.VehicleID = response.VehicleInfo.VehicleID;
                    JDPListOfAppliedOffersObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    JDPListOfAppliedOffersObj.Price = TrimStringValueToLimit(appliedOfferslist.Price,100);
                    JDPListOfAppliedOffersObj.Amount = Convert.ToDecimal(appliedOfferslist.Amount);
                    JDPListOfAppliedOffersObj.OfferName = TrimStringValueToLimit(appliedOfferslist.OfferName.ToString(),100);
                    JDPListOfAppliedOffersObj.OfferDescription = TrimStringValueToLimit(appliedOfferslist.OfferDescription,200);
                    JDPListOfAppliedOffersObj.OfferStartDate = appliedOfferslist.OfferStartDate;
                    JDPListOfAppliedOffersObj.OfferEndDate = appliedOfferslist.OfferEndDate;
                    JDPListOfAppliedOffersObj.Order = 0;
                    JDPListOfAppliedOffersObj.IsActive = true;
                    JDPListOfAppliedOffersObj.CreatedDate = DateTime.Now;
                    JDPListOfAppliedOffersObj.CreatedBy = 1;
                    JDPListOfAppliedOffersList.Add(JDPListOfAppliedOffersObj);
                }
            }
        }

        /// <summary>
        /// Functions for Map resposne to  JDP List Of Sub Options
        /// </summary>
        /// <returns></returns>
        /// 
        private static void InsertJDPSubOptions(List<JDPSubOptions> JDPSubOptionsList, Datum response)
        {
            if (response.PremiumOptions != null)
            {

                foreach (var PremiumOptionsObjlist in response.PremiumOptions)
                {

                    if (PremiumOptionsObjlist.SubOptions != null)
                    {
                        foreach (var JDPSubOptionsListObj in PremiumOptionsObjlist.SubOptions)
                        {

                            JDPSubOptions JDPSubOptionsObj = new JDPSubOptions();
                            JDPSubOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                            JDPSubOptionsObj.DealerID = response.VehicleInfo.DealerID;
                            JDPSubOptionsObj.VehicleID = response.VehicleInfo.VehicleID;
                            JDPSubOptionsObj.Name = TrimStringValueToLimit(JDPSubOptionsListObj.Name,100);
                            JDPSubOptionsObj.Msrp = Convert.ToDecimal(JDPSubOptionsListObj.Msrp);
                            JDPSubOptionsObj.Order = 0;

                            //JDPSubOptionsObj.Code = JDPSubOptionsObj.Code;
                            //JDPSubOptionsObj.IsChecked = JDPSubOptionsObj.IsChecked;
                            //JDPSubOptionsObj.OptionalOptionId = JDPSubOptionsObj.OptionalOptionId;
                            //JDPSubOptionsObj.packageID = JDPSubOptionsObj.packageID;
                            //JDPSubOptionsObj.packageParentID = JDPSubOptionsObj.packageParentID;

                            JDPSubOptionsObj.IsActive = true;
                            JDPSubOptionsObj.CreatedBy = 1;
                            JDPSubOptionsObj.CreatedDate = DateTime.Now;
                            JDPSubOptionsList.Add(JDPSubOptionsObj);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Functions for Map resposne to  JDP List Of PPricing
        /// </summary>
        /// <returns></returns>
        /// 
        private static JDPPricing InsertJDPPricing(Datum response)
        {
            JDPPricing objectJDPPricing = new JDPPricing();
            objectJDPPricing.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
            objectJDPPricing.VehicleID = response.VehicleInfo.VehicleID;
            objectJDPPricing.DealerID = response.VehicleInfo.DealerID;
            objectJDPPricing.Cost = response.Pricing.Cost;
            objectJDPPricing.ExtraPrice1 = response.Pricing.ExtraPrice1;
            objectJDPPricing.ExtraPrice2 = response.Pricing.ExtraPrice2;
            objectJDPPricing.ExtraPrice3 = response.Pricing.ExtraPrice3;
            objectJDPPricing.List = response.Pricing.List;
            objectJDPPricing.Special = response.Pricing.Special;
            objectJDPPricing.Order = 1;
            objectJDPPricing.IsActive = true;
            objectJDPPricing.CreatedDate = DateTime.Now;
            objectJDPPricing.CreatedBy = 1;
            return objectJDPPricing;
        }

        /// <summary>
        /// Functions for Map resposne to  JDP VehicleInfo
        /// </summary>
        /// <returns></returns>
        /// 
        private static JDPVehicleInfo InsertJDPVehicleInfo(Datum response ,string CalledBy)
        {
            JDPVehicleInfo objectvechicleinfo = new JDPVehicleInfo();
            objectvechicleinfo.VehicleID = response.VehicleInfo.VehicleID;
            objectvechicleinfo.DealerID = response.VehicleInfo.DealerID;
            objectvechicleinfo.DealerName = TrimStringValueToLimit(response.VehicleInfo.DealerName,100);
            objectvechicleinfo.IsNew = response.VehicleInfo.IsNew;
            objectvechicleinfo.VIN = TrimStringValueToLimit(response.VehicleInfo.VIN,17);
            objectvechicleinfo.StockNumber = TrimStringValueToLimit(response.VehicleInfo.StockNumber,100);
            objectvechicleinfo.IsCertified = response.VehicleInfo.IsCertified;
            objectvechicleinfo.Year = (response.VehicleInfo.Year == 0 ? 0000 : response.VehicleInfo.Year);
            objectvechicleinfo.Make = TrimStringValueToLimit(response.VehicleInfo.Make,100);
            objectvechicleinfo.Model = TrimStringValueToLimit(response.VehicleInfo.Model,100);
            objectvechicleinfo.ModelCode = response.VehicleInfo.ModelCode;
            objectvechicleinfo.Trim = TrimStringValueToLimit(response.VehicleInfo.Trim,100);
            objectvechicleinfo.BodyName = TrimStringValueToLimit((response.VehicleInfo.BodyName == null ? "" : response.VehicleInfo.BodyName.ToString()),100);
            objectvechicleinfo.BodyStyle = TrimStringValueToLimit(response.VehicleInfo.BodyStyle.ToString(),100);
            objectvechicleinfo.CityMPG = response.VehicleInfo.CityMPG;
            objectvechicleinfo.HwyMPG = response.VehicleInfo.HwyMPG;
            objectvechicleinfo.DaysInSotck = response.VehicleInfo.DaysInSotck;
            objectvechicleinfo.ValueSource = TrimStringValueToLimit(response.VehicleInfo.ValueSource,100);
            objectvechicleinfo.ExteriorColor = TrimStringValueToLimit(response.VehicleInfo.ExteriorColor,100);
            objectvechicleinfo.InteriorColor = TrimStringValueToLimit(response.VehicleInfo.InteriorColor,100);
            objectvechicleinfo.InteriorMaterial = TrimStringValueToLimit(response.VehicleInfo.InteriorMaterial,100);
            objectvechicleinfo.Engine = TrimStringValueToLimit(response.VehicleInfo.Engine,100);

            objectvechicleinfo.Transmission = TrimStringValueToLimit(response.VehicleInfo.Transmission, 100);
            objectvechicleinfo.TransmissionSpeed = TrimStringValueToLimit(response.VehicleInfo.TransmissionSpeed, 100);

            objectvechicleinfo.InStockDate = response.VehicleInfo.InStockDate;
            objectvechicleinfo.LastModifiedDate = response.VehicleInfo.LastModifiedDate;
            objectvechicleinfo.IsSpecial = (response.VehicleInfo.IsSpecial == null ? false : Convert.ToBoolean(response.VehicleInfo.IsSpecial));
            objectvechicleinfo.BodyType = TrimStringValueToLimit(response.VehicleInfo.BodyType,100);
            objectvechicleinfo.Locked = response.VehicleInfo.Locked;
            objectvechicleinfo.VehicleStatus = TrimStringValueToLimit(response.VehicleInfo.VehicleStatus,100);
            objectvechicleinfo.DealerCertified1 = response.VehicleInfo.DealerCertified1;
            objectvechicleinfo.DealerCertified2 = response.VehicleInfo.DealerCertified2;
            objectvechicleinfo.GenericExteriorColor = TrimStringValueToLimit((response.VehicleInfo.GenericExteriorColor == null ? "" : response.VehicleInfo.GenericExteriorColor.ToString()),100);
            objectvechicleinfo.VideoUrl = TrimStringValueToLimit(response.VehicleInfo.VideoUrl,200);
            objectvechicleinfo.Drivetrain = TrimStringValueToLimit(response.VehicleInfo.Drivetrain,100);
            objectvechicleinfo.Mileage = response.VehicleInfo.Mileage;
            objectvechicleinfo.Category1 = TrimStringValueToLimit((response.VehicleInfo.Category1 == null ? "" : response.VehicleInfo.Category1.ToString()),100);
            objectvechicleinfo.Category2 = TrimStringValueToLimit((response.VehicleInfo.Category2 == null ? "" : response.VehicleInfo.Category2.ToString()),100);
            objectvechicleinfo.Category3 = TrimStringValueToLimit((response.VehicleInfo.Category3 == null ? "" : response.VehicleInfo.Category3.ToString()),100);
            objectvechicleinfo.Category4 = TrimStringValueToLimit((response.VehicleInfo.Category4 == null ? "" : response.VehicleInfo.Category4.ToString()),100);
            objectvechicleinfo.Category5 = TrimStringValueToLimit((response.VehicleInfo.Category5 == null ? "" : response.VehicleInfo.Category5.ToString()),100);
            objectvechicleinfo.Style = TrimStringValueToLimit(response.VehicleInfo.Style,100);
            objectvechicleinfo.ChromeStyleID = response.VehicleInfo.ChromeStyleID;
            objectvechicleinfo.ZipCode = TrimStringValueToLimit(response.VehicleInfo.ZipCode,100);
            objectvechicleinfo.ExportDealerID = TrimStringValueToLimit(response.VehicleInfo.ExportDealerID,100);
            objectvechicleinfo.EngineFuelType = TrimStringValueToLimit(response.VehicleInfo.EngineFuelType,100);
            objectvechicleinfo.ExteriorColorCode = TrimStringValueToLimit((response.VehicleInfo.ExteriorColorCode == null ? "" : response.VehicleInfo.ExteriorColorCode.ToString()),100);
            objectvechicleinfo.ExteriorGenericColorDescription = TrimStringValueToLimit((response.VehicleInfo.ExteriorGenericColorDescription == null ? "" : response.VehicleInfo.ExteriorGenericColorDescription.ToString()),100);
            objectvechicleinfo.IsActive = true;
            objectvechicleinfo.CreatedDate = DateTime.Now;
            objectvechicleinfo.CreatedBy = 99;
            objectvechicleinfo.IsInternalSynch = false;
            objectvechicleinfo.SynchedBy = 1;
            objectvechicleinfo.InternalID = 1;
            objectvechicleinfo.APICalledBy = CalledBy;
            //objectvechicleinfo.OneOwner = (response.VehicleInfo.OneOwner == null ? "" : response.VehicleInfo.OneOwner.ToString());
            //objectvechicleinfo.VehicleHistoryReportLink = (response.VehicleInfo.VehicleHistoryReportLink == null ? "" : response.VehicleInfo.VehicleHistoryReportLink.ToString());
            //objectvechicleinfo.InteriorColorCode = (response.VehicleInfo.InteriorColorCode == null ? "" : response.VehicleInfo.InteriorColorCode.ToString());
            //objectvechicleinfo.DoorCount = (response.VehicleInfo.DoorCount == null ? "" : response.VehicleInfo.DoorCount.ToString());
            //objectvechicleinfo.EngineDisplacement = (response.VehicleInfo.EngineDisplacement == null ? "" : response.VehicleInfo.EngineDisplacement.ToString());
            return objectvechicleinfo;
        }


        /// <summary>
        /// Functions for Map resposne to  JDP Extended Descriptions
        /// </summary>
        /// <returns></returns>
        ///
        private static JDPExtendedDescriptions InsertJDPExtendedDescriptions(Datum response)
        {
            JDPExtendedDescriptions JDPExtendedDescriptionsObj = new JDPExtendedDescriptions();

            JDPExtendedDescriptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
            JDPExtendedDescriptionsObj.DealerID = response.VehicleInfo.DealerID;
            JDPExtendedDescriptionsObj.VehicleID = response.VehicleInfo.VehicleID;
            JDPExtendedDescriptionsObj.ExtendedVehicleName = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedVehicleName,100);
            JDPExtendedDescriptionsObj.ExtendedBodyStyle = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedBodyStyle,100);
            JDPExtendedDescriptionsObj.ExtendedDrivetrain = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedDrivetrain,100);
            JDPExtendedDescriptionsObj.ExtendedDisplacement = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedDisplacement,100);
            JDPExtendedDescriptionsObj.ExtendedEngineType = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedEngineType,100);
            JDPExtendedDescriptionsObj.ExtendedFuelSystem = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedFuelSystem,100);
            JDPExtendedDescriptionsObj.ExtendedTransmissionDescripCont = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedTransmissionDescripCont,100);
            JDPExtendedDescriptionsObj.ExtendedTransType = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedTransType,100);
            JDPExtendedDescriptionsObj.ExtendedBestmakename = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedBestmakename,100);
            JDPExtendedDescriptionsObj.ExtendedBestmodelname = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedBestmodelname,100);
            JDPExtendedDescriptionsObj.ExtendedBeststylename = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedBeststylename,100);
            JDPExtendedDescriptionsObj.ExtendedBesttrimname = TrimStringValueToLimit(response.VehicleInfo.ExtendedDescriptions.ExtendedBesttrimname,100);
            JDPExtendedDescriptionsObj.IsActive = true;
            JDPExtendedDescriptionsObj.CreatedDate = DateTime.Now;
            JDPExtendedDescriptionsObj.CreatedBy = 1;
            return JDPExtendedDescriptionsObj;
        }

        /// <summary>
        /// Functions for Map resposne to  JDP Photos
        /// </summary>
        /// <returns></returns>
        ///
        private static void InsertJDPListOfPhotos(List<JDPListOfPhotos> JDPListOfPhotosList, Datum response)
        {

            if (response.ListOfPhotos != null)
            {
                foreach (var photolist in response.ListOfPhotos)
                {

                    JDPListOfPhotos VehiclePhotosListObj = new JDPListOfPhotos();
                    VehiclePhotosListObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    VehiclePhotosListObj.DealerID = response.VehicleInfo.DealerID;
                    VehiclePhotosListObj.VehicleID = response.VehicleInfo.VehicleID;
                    VehiclePhotosListObj.VehiclePhotoID = TrimStringValueToLimit(photolist.VehiclePhotoID,100);
                    VehiclePhotosListObj.PhotoUrl = TrimStringValueToLimit(photolist.PhotoUrl,200);
                    VehiclePhotosListObj.Order = photolist.Order;
                    VehiclePhotosListObj.PhotoTimestamp = photolist.PhotoTimestamp;
                    VehiclePhotosListObj.IsActive = true;
                    VehiclePhotosListObj.CreatedDate = DateTime.Now;
                    VehiclePhotosListObj.CreatedBy = 1;
                    JDPListOfPhotosList.Add(VehiclePhotosListObj);
                }
            }
        }


        /// <summary>
        /// Functions for Map resposne to Vehicle Comments
        /// </summary>
        /// <returns></returns>
        ///
        private static JDPVehicleComments InsertJDPVehicleComments(Datum response)
        {
            JDPVehicleComments objecVehicleComments = new JDPVehicleComments();

            objecVehicleComments.VehicleID = response.VehicleInfo.VehicleID;
            objecVehicleComments.DealerID = response.VehicleInfo.DealerID;
            objecVehicleComments.Comments = TrimStringValueToLimit(response.VehicleInfo.Comments,999);
            objecVehicleComments.Comments2 = TrimStringValueToLimit(response.VehicleInfo.Comments2,999);
            objecVehicleComments.Comments3 = TrimStringValueToLimit(response.VehicleInfo.Comments3,999);
            objecVehicleComments.Comments4 = TrimStringValueToLimit(response.VehicleInfo.Comments4,999);
            objecVehicleComments.Comments5 = TrimStringValueToLimit(response.VehicleInfo.Comments5,999);
            objecVehicleComments.CreatedBy = 1;
            objecVehicleComments.IsActive = true;
            objecVehicleComments.CreatedDate = DateTime.Now;
            return objecVehicleComments;
        }

        /// <summary>
        /// Functions for Map resposne to Premium Options
        /// </summary>
        /// <returns></returns>
        ///
        private static void InsertJDPPremiumOptions(List<JDPPremiumOptions> JDPPremiumOptionsList, Datum response)
        {

            if (response.PremiumOptions != null)
            {
                foreach (var PremiumOptionsObjlist in response.PremiumOptions)
                {

                    JDPPremiumOptions PremiumOptionsObj = new JDPPremiumOptions();
                    PremiumOptionsObj.JDPVehicleInfoId = response.VehicleInfo.VehicleID;
                    PremiumOptionsObj.DealerID = response.VehicleInfo.DealerID;
                    PremiumOptionsObj.VehicleID = response.VehicleInfo.VehicleID;

                    // PremiumOptionsObj.Order = PremiumOptionsObjlist.Order;

                    PremiumOptionsObj.Code = TrimStringValueToLimit(PremiumOptionsObjlist.Code, 100);
                    PremiumOptionsObj.Name = TrimStringValueToLimit(PremiumOptionsObjlist.Name, 100);
                    PremiumOptionsObj.Order = 1;

                    PremiumOptionsObj.IsActive = true;
                    PremiumOptionsObj.CreatedBy = 1;
                    PremiumOptionsObj.CreatedDate = DateTime.Now;
                    JDPPremiumOptionsList.Add(PremiumOptionsObj);
                }
            }
        }

        #endregion

        /// <summary>
        /// Functions for Get AllV ehicle In formations
        /// </summary>
        /// <returns></returns>
        ///
        [Authorize]
        [HttpPost]
        [Route("GetAllVehicleInformations")]
        public IActionResult GetAllVehicleInformations([FromBody] JDPSearchDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //var responseObj = _IICCBatchLogicBAL.GetAllVehicleInformation(Obj.DealerId, Obj.Make, Obj.Opcode);
                var responseObj = _IICCBatchLogicBAL.GetAllVehicleInformationbypage(Obj.DealerId, Obj.Make, Obj.Opcode, Obj.PageNumber, Obj.RowsOfPage);
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
        /// Functions for Get Dealer Informations
        /// </summary>
        /// <returns></returns>
        ///

        [Authorize]
        [HttpGet]
        [Route("GetDealerInformations")]
        public IActionResult GetDealerInformations()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _IICCBatchLogicBAL.GetDealerInfo();
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
        /// Functions forGet Vehicle CountBy DealerIds
        /// </summary>
        /// <returns></returns>
        ///

        [Authorize]
        [HttpGet]
        [Route("GetVehicleCountByDealerIds")]
        public IActionResult GetVehicleCountByDealerIds()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _IICCBatchLogicBAL.GetVehicleCountByDealerId();
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
        /// Functions forGet Get Dealer Info For Searchs filter
        /// </summary>
        /// <returns></returns>
        ///
       [Authorize]
        [HttpGet]
        [Route("GetDealerInfoForSearchs")]
        public IActionResult GetDealerInfoForSearchs()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _IICCBatchLogicBAL.GetDealerInfoForSearch();
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
        /// Functions forGet Get Make Info For Searchs filter
        /// </summary>
        /// <returns></returns>
        ///
        [Authorize]
        [HttpGet]
        [Route("GetVehicleMakeForSearchs")]
        public IActionResult GetVehicleMakeForSearchs()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _IICCBatchLogicBAL.GetVehicleMakeForSearch();
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
        /// Functions for  Getting API call history
        /// </summary>
        /// <returns></returns>
        ///
      [Authorize]
        [HttpGet]
        [Route("GetJDPAPICallHistoryList")]
        public IActionResult GetJDPAPICallHistoryList()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

            
                var responseObj = _IICCBatchLogicBAL.GetJDPAPICallHistory();
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
        /// Functions for Get Vehicle info by id
        /// </summary>
        /// <returns></returns>
        ///
        [Authorize]
        [HttpPost]
        [Route("GetJDPVehicleInfoByVehicleId")]
        public IActionResult GetJDPVehicleInfoByVehicleId([FromBody] JDPVehicleInfoDTO jDPVehicleInfoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetJDPVehicleInfoByVehicleIds(jDPVehicleInfoDTO.VehicleID.ToString());

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
        /// Functions for Get Sub Opitons by VehicleId
        /// </summary>
        /// <returns></returns>
        ///

        [Authorize]
        [HttpPost]
        [Route("GetVehicleSubOpitonsByVehicleId")]
        public IActionResult GetVehicleSubOpitonsByVehicleId([FromBody] JDPSubOptionsDTO jDPSubOptionsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehicleSubOpitonsByVehicleIds(jDPSubOptionsDTO.VehicleID.ToString());

                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CleanAllJDPPowerAppTables")]
        public IActionResult CleanAllJDPPowerAppTables([FromBody] JDPSearchDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var responseObj = _IICCBatchLogicBAL.CleanAllJDPPowerAppTable();
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
        /// Functions for Get Applied Offers by VehicleId
        /// </summary>
        /// <returns></returns>
        ///
        [Authorize]
        [HttpPost]
        [Route("GetVehicleAppliedOffersByVehicleId")]
        public IActionResult GetVehicleAppliedOffersByVehicleId([FromBody] JDPListOfAppliedOffersDTO jDPListOfAppliedOffersDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehicleAppliedOffersByVehicleIds(jDPListOfAppliedOffersDTO.VehicleID.ToString());

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
        /// Functions for Get Pricing by VehicleId
        /// </summary>
        /// <returns></returns>
        ///

        [Authorize]
        [HttpPost]
        [Route("GetVehiclePricingByVehicleId")]
        public IActionResult GetVehiclePricingByVehicleId([FromBody] JDPPricingDTO pricingDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var responseObj = _IICCBatchLogicBAL.GetVehiclePricingByVehicleIds(pricingDTO.VehicleID.ToString());

                if (responseObj == null)
                    return NotFound();
                return Ok(responseObj);
            }
            catch (AppException ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
