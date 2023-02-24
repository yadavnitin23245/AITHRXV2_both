


using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AirthwholesaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemovetextAPIController : ControllerBase
    {
        private string _token;
        public string _saveimagepath;
        public string _originpath;
        public string _BaseAddress;
        public string _callaspnetapi;
        private readonly JDPAPIDbContext _JDPcontext;
        private readonly IRepository<JDPListOfPhotos> _jDPListOfPhotos;
        IOptions<RemovetextDTOConfig> _settings;

        protected IRemoveTextDashbaordLogic _IRemoveTextDashbaordLogic { get; private set; }
        public RemovetextAPIController(IOptions<RemovetextDTOConfig> settings, JDPAPIDbContext JDPcontext,
            IRepository<JDPListOfPhotos> jDPListOfPhotos,
            IRemoveTextDashbaordLogic IRemoveTextDashbaordLogic
            )
        {
            _token = settings.Value.ApplicationName;
            _saveimagepath = settings.Value.saveimagepath;
            _originpath = settings.Value.originpath;
            _BaseAddress = settings.Value.BaseAddress;
            _JDPcontext = JDPcontext;
            _jDPListOfPhotos = jDPListOfPhotos;
            _callaspnetapi = settings.Value.callaspnetapi;
            _settings = settings;
            _IRemoveTextDashbaordLogic = IRemoveTextDashbaordLogic;

        }

        //read file from saved folder and convert into base 64 and display
        [HttpGet]
        [Route("readImagefile")]
        public string readImagefile(string imagepath)
        {
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                var img = string.Format("data:image/jpg;base64,{0}", base64ImageRepresentation);
                return img;
            }
            catch (Exception ex)
            {
                return "nul";
            }
        }

        #region downloadImageFromCdn

        //save image from cdn and post all data to python api
        //[HttpPost]
        //[Route("downloadImageFromCdn")]
        //public IActionResult downloadImageFromCdn(string[] Cdnlinks)
        //{
        //    //var imageurl = "https://cdn.inventoryrsc.com/234266646_639145ee483eea183e9eb1f5.jpg";


        //    try
        //    {



        //        List<string> downloadedimagepath = new List<string>();
        //        foreach (var cdnlinkobj in Cdnlinks)
        //        {
        //            //function call to get the filename
        //            string filename = Path.GetFileName(cdnlinkobj);

        //            // string saveimagepath = "C:\\Python Djngo project\\To_be_clean\\";

        //            //this path for development
        //            // string saveimagepath = "C:\\inetpub\\wwwroot\\demodjango\\To_be_clean\\";

        //            string saveimagepath = _saveimagepath;
        //            using (WebClient client = new WebClient())
        //            {
        //                client.DownloadFile(new Uri(cdnlinkobj), saveimagepath + filename);

        //                //posting image paths to removetext api
        //                var sourecimagepath = saveimagepath + filename;

        //                downloadedimagepath.Add(sourecimagepath);
        //                //  CallApi(sourecimagepath);
        //            }

        //        }

        //        var ListFilepath = CallApi_arrayfiles(downloadedimagepath);
        //        return Ok(ListFilepath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //}



        //post array file
        //Call Python remove text APi
        //[HttpGet]
        //[Route("CallApi_arrayfiles")]
        //public List<RemoveTextResponse> CallApi_arrayfiles(List<string> filepath)
        //{
        //    List<RemoveTextResponse> Removtextfilelist = new List<RemoveTextResponse>();
        //    try
        //    {

        //        // Populate the form variable
        //        var formVariables = new List<KeyValuePair<string, string>>();

        //        foreach (var variable in filepath)
        //        {
        //            formVariables.Add(new KeyValuePair<string, string>("Imagepath", variable));
        //        }
        //        var formContent = new FormUrlEncodedContent(formVariables);

        //        using (var client = new HttpClient())
        //        {
        //            client.Timeout = TimeSpan.FromMinutes(20);

        //            var baseadressurl = _BaseAddress;
        //            // client.BaseAddress = new Uri("http://127.0.0.1:8000/"); //for local

        //            // client.BaseAddress = new Uri("http://pythonremovetext.aithrx.com/"); //for live
        //            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            client.BaseAddress = new Uri(baseadressurl); //for live

        //            //HTTP GET
        //            var responseTask = client.PostAsync("Uploadarray/", formContent);
        //            responseTask.Wait();

        //            var result = responseTask.Result;
        //            if (result.IsSuccessStatusCode)
        //            {

        //                //var readTask = result.Content.ReadAsAsync<Student[]>();
        //                var readTask = result.Content.ReadAsStringAsync();
        //                readTask.Wait();

        //                var Responsedata = readTask.Result;

        //                Removtextfilelist = JsonConvert.DeserializeObject<List<RemoveTextResponse>>(Responsedata);


        //            }
        //        }
        //        return Removtextfilelist;

        //    }
        //    catch (Exception ex)
        //    {
        //        RemoveTextResponse errroobj = new RemoveTextResponse();

        //        errroobj.error_details = ex.Message.ToString();
        //        Removtextfilelist.Add(errroobj);


        //        return Removtextfilelist;
        //    }
        //}


        //[Route("Callapiforremovetext_iis")]
        //[HttpGet]
        //public IActionResult Callapiforremovetext_iis()
        //{

        //    List<RemoveTextResponse> Removtextfilelist = new List<RemoveTextResponse>();
        //    try
        //    {

        //        var cdnlinkfromdb = _jDPListOfPhotos.GetAll().Take(10);
        //        // Populate the form variable
        //        var formVariables = new List<KeyValuePair<string, string>>();

        //        foreach (var variable in cdnlinkfromdb)
        //        {
        //            formVariables.Add(new KeyValuePair<string, string>("Cdnlinks", variable.PhotoUrl));
        //        }
        //        var formContent = new FormUrlEncodedContent(formVariables);

        //        using (var client = new HttpClient())
        //        {
        //            client.Timeout = TimeSpan.FromMinutes(20);

        //            var baseadressurl = _callaspnetapi;
        //            // client.BaseAddress = new Uri("http://127.0.0.1:8000/"); //for local

        //            // client.BaseAddress = new Uri("http://pythonremovetext.aithrx.com/"); //for live
        //            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

        //            client.BaseAddress = new Uri(baseadressurl); //for live

        //            //HTTP GET
        //            var responseTask = client.PostAsync("RemoveTextfromImage/downloadImageFromCdn/", formContent);
        //            responseTask.Wait();

        //            var result = responseTask.Result;
        //            if (result.IsSuccessStatusCode)
        //            {

        //                //var readTask = result.Content.ReadAsAsync<Student[]>();
        //                var readTask = result.Content.ReadAsStringAsync();
        //                readTask.Wait();

        //                var Responsedata = readTask.Result;

        //                Removtextfilelist = JsonConvert.DeserializeObject<List<RemoveTextResponse>>(Responsedata);


        //            }
        //        }
        //        //  return Removtextfilelist;

        //        return Ok();

        //    }
        //    catch (Exception ex)
        //    {
        //        RemoveTextResponse errroobj = new RemoveTextResponse();

        //        errroobj.error_details = ex.Message.ToString();
        //        Removtextfilelist.Add(errroobj);


        //        // return Removtextfilelist;
        //        return Ok();
        //    }


        //    return Ok();

        //}
        #endregion

        [Route("CallRemoveTextapi")]
        [HttpGet]
        public async Task<IActionResult> Calling_Remove_TextAPI()
        {
            List<RemoveTextResponse> Removtextfilelist = new List<RemoveTextResponse>();

            // var cdnfromdb = _JDPcontext.JDPListOfPhotos.Where(i=>i.CleanedPhotourl==null && i.IsActive==true).ToList().Take(20);
            var cdnfromdb = _IRemoveTextDashbaordLogic.GetimagesForClean("");
            foreach (var cdnlink in cdnfromdb)
            {
                List<ParamforpostAPI> Paramobjlist = new List<ParamforpostAPI>();

                ParamforpostAPI paramobj = new ParamforpostAPI();

                paramobj.cdnlink = cdnlink.PhotoUrl;
                paramobj.Imag_Id = cdnlink.VehiclePhotoID;
                paramobj.sortorder = cdnlink.Order;


                Paramobjlist.Add(paramobj);
                var json = System.Text.Json.JsonSerializer.Serialize(Paramobjlist);

                try
                {

                    //  var request = (HttpWebRequest)WebRequest.Create("http://removetextapi.aithrx.com/RemoveTextfromImage/downloadImageFromCdn/");

                    var request = (HttpWebRequest)WebRequest.Create(_settings.Value.PythonAPIurl);


                    //var postData = "Cdnlinks=" + Uri.EscapeDataString("https://cdn.inventoryrsc.com/106124578_5f6bb171cb11a031ce7284cd.jpg");
                    //postData += "&Cdnlinks=" + Uri.EscapeDataString("https://cdn.inventoryrsc.com/106124578_5f6bb171cb11a031ce7284ce.jpg");

                    // var postData = "Cdnlinks=" + Uri.EscapeDataString(cdnlink.PhotoUrl);

                    var data = Encoding.ASCII.GetBytes(json);

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)await request.GetResponseAsync();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


                  var  RemovtextfilelistResponse = JsonConvert.DeserializeObject<List<RemoveTextResponse>>(responseString);


                    if (RemovtextfilelistResponse.Count > 0)
                    {
                        var matchedphoto = _JDPcontext.JDPListOfPhotos.Where(i => i.VehiclePhotoID == RemovtextfilelistResponse.FirstOrDefault().Image_id).FirstOrDefault();

                        matchedphoto.CleanedPhotourl = RemovtextfilelistResponse.FirstOrDefault().Removed_Text_file_path;
                        _JDPcontext.SaveChanges();
                        Removtextfilelist.AddRange(RemovtextfilelistResponse);

                        //inserting response in other table
                        var removetextisExist = _JDPcontext.JDPPhotoCleanedDetails.Where(i => i.Image_id == RemovtextfilelistResponse.FirstOrDefault().Image_id).FirstOrDefault();

                        if(removetextisExist==null)
                        {
                            JDPPhotoCleanedDetails textremoveobject = new JDPPhotoCleanedDetails();

                            textremoveobject.Image_id = RemovtextfilelistResponse.FirstOrDefault().Image_id;
                            textremoveobject.new_file_size= RemovtextfilelistResponse.FirstOrDefault().new_file_size;
                            textremoveobject.old_file_size = RemovtextfilelistResponse.FirstOrDefault().old_file_size;
                            textremoveobject.orignalPhotocdnlink = cdnlink.PhotoUrl;
                            textremoveobject.removetextfilecdnlink = "";
                            textremoveobject.characters_value = RemovtextfilelistResponse.FirstOrDefault().characters_value;
                            textremoveobject.Removetext_count = !string.IsNullOrEmpty(RemovtextfilelistResponse.FirstOrDefault().Removetext_count)?Convert.ToInt32(RemovtextfilelistResponse.FirstOrDefault().Removetext_count):0;
                            textremoveobject.error = RemovtextfilelistResponse.FirstOrDefault().error;
                            textremoveobject.Old_file_name = RemovtextfilelistResponse.FirstOrDefault().Old_file_name;
                            textremoveobject.Removed_file_name = RemovtextfilelistResponse.FirstOrDefault().Removed_file_name;
                            textremoveobject.Removed_Text_file_path = RemovtextfilelistResponse.FirstOrDefault().Removed_Text_file_path;
                            textremoveobject.Source_file_path = RemovtextfilelistResponse.FirstOrDefault().Source_file_path;
                            textremoveobject.VehicleID = cdnlink.VehicleID;
                            textremoveobject.CreatedDate = DateTime.Now;
                            textremoveobject.sortorder = RemovtextfilelistResponse.FirstOrDefault().sortorder;

                            _JDPcontext.JDPPhotoCleanedDetails.Add(textremoveobject);
                            _JDPcontext.SaveChanges();

                        }
                    }
                    //return null;
                }

                catch (Exception ex)
                {

                }

            }
            return Ok(Removtextfilelist);
        }


        #region uplaodfromfolderToazurestorage_backup
        //upload file to storage
        //[Route("uplaodfromfolderToazurestorage_backup")]
        //[HttpGet]
        //public async Task<IActionResult> uplaodfromfolderToazurestorage_backup()
        //{
        //    var storage = new AzureBlobStorage(_settings);



        //    // Read a local file
        //    foreach (string Filesname in Directory.GetFiles(@"C:\Python Djngo project\To_be_clean\"))
        //    {
        //        var Filesnamedata = "d:\\sample.jpg";

        //        using (FileStream file = System.IO.File.Open(Filesnamedata, FileMode.Open))
        //        {
        //            try
        //            {
        //                string filename = Path.GetFileName(Filesnamedata);
        //                var filenameforstroage = GenerateFileName(Filesnamedata);

        //                // Pattern to run an async code from a sync method
        //                //storage.Create(file, filenameforstroage).ContinueWith(t =>
        //                //{
        //                //    if (t.IsCompletedSuccessfully)
        //                //    {
        //                //        Console.Out.WriteLine("Blob uploaded");
        //                //    }
        //                //}).Wait();

        //                var responseResult = await storage.Create(file, filenameforstroage);
        //                //return responseResult;

        //            }
        //            catch (Exception e)
        //            {
        //                // Omitted
        //            }
        //        }

        //    }
        //    return Ok();
        //}



        //upload file to storage

        #endregion

        [Route("uplaodfromfolderToazurestorage")]
        [HttpGet]
        public async Task<IActionResult> uplaodfromfolderToazurestorage()
        {
            var storage = new AzureBlobStorage(_settings);

            var removetextfolderfiles=_JDPcontext.JDPListOfPhotos.Where(i=>i.CleanedPhotourl!=null && string.IsNullOrEmpty(i.RemovetextCDNlink)).ToList();

            // Read a local file
            foreach (var Filesname in removetextfolderfiles)
            {
                var Filesnamedata = _settings.Value.pythonProjectpath+ Filesname.CleanedPhotourl;

                using (FileStream file = System.IO.File.Open(Filesnamedata, FileMode.Open))
                {
                    try
                    {
                        string filename = Path.GetFileName(Filesnamedata);
                        var filenameforstroage = GenerateFileName(filename);

                        // Pattern to run an async code from a sync method
                        //storage.Create(file, filenameforstroage).ContinueWith(t =>
                        //{
                        //    if (t.IsCompletedSuccessfully)
                        //    {
                        //        Console.Out.WriteLine("Blob uploaded");
                        //    }
                        //}).Wait();

                        var responseResult = await storage.Create(file, filenameforstroage);

                        var cdnRemovetextlink= _settings.Value.azureblob_cdnlink.ToString() + responseResult;

                        Filesname.RemovetextCDNlink = cdnRemovetextlink;
                        _JDPcontext.SaveChanges();

                        //remove text updated remove cdn link
                        var removetextisExist = _JDPcontext.JDPPhotoCleanedDetails.Where(i => i.Image_id == Filesname.VehiclePhotoID).FirstOrDefault();

                        if(removetextisExist!=null)
                        {
                            removetextisExist.removetextfilecdnlink = cdnRemovetextlink;
                            removetextisExist.updatecdndate = DateTime.Now;
                            _JDPcontext.SaveChanges();
                        }
                        //return responseResult;

                    }
                    catch (Exception e)
                    {
                        // Omitted
                    }
                }

            }
            return Ok();
        }




        [Route("GenerateFileName")]
        [HttpGet]
        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + strName[0] + "_" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
    }
}
