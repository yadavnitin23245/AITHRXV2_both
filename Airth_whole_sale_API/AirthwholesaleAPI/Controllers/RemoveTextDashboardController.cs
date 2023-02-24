using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Microsoft.AspNetCore.Mvc;

namespace AirthwholesaleAPI.Controllers
{
   
    public class RemoveTextDashboardController : Controller
    {
        private readonly JDPAPIDbContext _JDPcontext;

        protected IRemoveTextDashbaordLogic _IRemoveTextDashbaordLogic { get; private set; }
        public RemoveTextDashboardController(JDPAPIDbContext JDPcontext, IRemoveTextDashbaordLogic IRemoveTextDashbaordLogic)
        {
            _JDPcontext = JDPcontext;
            _IRemoveTextDashbaordLogic= IRemoveTextDashbaordLogic;
        }
        public IActionResult Dashboard()
        {
            List<RemoveTextFiledetailsDTO> Listfieldetails = new List<RemoveTextFiledetailsDTO>();
            var removetextdetails = _JDPcontext.JDPListOfPhotos.Where(i => i.RemovetextCDNlink != null).ToList();

            foreach(var filedata in removetextdetails)
            {
                RemoveTextFiledetailsDTO objdetails = new RemoveTextFiledetailsDTO();

                objdetails.removepiccdn = filedata.RemovetextCDNlink;
                objdetails.Orignalpiccdn = filedata.PhotoUrl;

                Listfieldetails.Add(objdetails);
            }

            return View(Listfieldetails);
        }


        public IActionResult DashboardFileDetails()
        {

            var DashboardDetails = _IRemoveTextDashbaordLogic.GetRemovtextDashboard("1");

            var CleanImagesDetails = _IRemoveTextDashbaordLogic.GetCleanImagesDetails("1");

            var uplaoded_azure_or_conberted_to_cdn = _JDPcontext.JDPPhotoCleanedDetails.Where(i => i.removetextfilecdnlink != null).ToList();

            CleanImagesDetailsDTO CleanImagesDetailsviewmodel = new CleanImagesDetailsDTO();

            DashboardFilesDetailsViewModelDTO objviewmodel = new DashboardFilesDetailsViewModelDTO();
            foreach (var cdnconvertedfiles in uplaoded_azure_or_conberted_to_cdn)
            {
                RemoveTextFiledetailsDTO objremovetext = new RemoveTextFiledetailsDTO();

                objremovetext.removetextfilecdnlink = cdnconvertedfiles.removetextfilecdnlink;
                objremovetext.orignalPhotocdnlink = cdnconvertedfiles.orignalPhotocdnlink;
                objremovetext.characters_value = (!string.IsNullOrEmpty(cdnconvertedfiles.characters_value)?cdnconvertedfiles.characters_value.Substring(1):"");
                objremovetext.Removetext_count = cdnconvertedfiles.Removetext_count;
                objremovetext.old_file_size = cdnconvertedfiles.old_file_size;
                objremovetext.new_file_size = cdnconvertedfiles.new_file_size;

                objviewmodel.RemoveTextFiledetailsDTO.Add(objremovetext);

            }


         

            objviewmodel.TotalFilescount= DashboardDetails.TotalFilescount;
            objviewmodel.TotalRemovedTextfilecount = DashboardDetails.TotalRemovedTextfilecount;
            objviewmodel.Total_file_uploadedToazure = DashboardDetails.Total_file_uploadedToazure;


            var TotalfileRemovedtext_percentage = (objviewmodel.TotalRemovedTextfilecount / objviewmodel.TotalFilescount) * 100;
            var TotalfileUploaded_azurePercentage = (objviewmodel.Total_file_uploadedToazure / objviewmodel.TotalFilescount) * 100;

            objviewmodel.TotalRemovedTextfilecount_Percentage = TotalfileRemovedtext_percentage;
            objviewmodel.TotalfileUploaded_azurePercentage = TotalfileUploaded_azurePercentage;

            ViewBag.CleanImagesDetailsList = CleanImagesDetails;

            return View(objviewmodel);
        }




        public IActionResult CleanedPictureDetailsByVechicleId(int vehicle)
        {
            //int vehcileIdint = 0;
            //if(vechicleId!=null)
            //{
            //    vehcileIdint = Convert.ToInt32(vechicleId);
            //}

            var uplaoded_azure_or_conberted_to_cdn = _JDPcontext.JDPPhotoCleanedDetails.Where(i => i.removetextfilecdnlink != null && i.removetextfilecdnlink != "" && i.VehicleID == vehicle).ToList();

            CleanImagesDetailsDTO CleanImagesDetailsviewmodel = new CleanImagesDetailsDTO();

            DashboardFilesDetailsViewModelDTO objviewmodel = new DashboardFilesDetailsViewModelDTO();
            foreach (var cdnconvertedfiles in uplaoded_azure_or_conberted_to_cdn)
            {
                RemoveTextFiledetailsDTO objremovetext = new RemoveTextFiledetailsDTO();

                objremovetext.removetextfilecdnlink = cdnconvertedfiles.removetextfilecdnlink;
                objremovetext.orignalPhotocdnlink = cdnconvertedfiles.orignalPhotocdnlink;
                objremovetext.characters_value = (!string.IsNullOrEmpty(cdnconvertedfiles.characters_value)? cdnconvertedfiles.characters_value.Substring(1) : "");
                objremovetext.Removetext_count = cdnconvertedfiles.Removetext_count;
                objremovetext.old_file_size = cdnconvertedfiles.old_file_size;
                objremovetext.new_file_size = cdnconvertedfiles.new_file_size;

                objviewmodel.RemoveTextFiledetailsDTO.Add(objremovetext);

            }


            return View(objviewmodel);
        }
    }
}
