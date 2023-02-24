using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class RemoveTextFiledetailsDTO
    {
        public string Orignalpiccdn { get; set; }

        public string removepiccdn { get; set; }


        public int RemovetextId { get; set; }
        public string Source_file_path { get; set; }
        public string Processed_file_path { get; set; }
        public string Removed_Text_file_path { get; set; }
        public string Old_file_name { get; set; }
        public string Removed_file_name { get; set; }
        public string old_file_size { get; set; }
        public string new_file_size { get; set; }
        public string error { get; set; }

        public string error_details { get; set; }

        public string characters_value { get; set; }
        public int? Removetext_count { get; set; }

        public string Image_id { get; set; }

        public string orignalPhotocdnlink { get; set; }
        public string removetextfilecdnlink { get; set; }
        public long? VehicleID { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? updatecdndate { get; set; }

        public int? sortorder { get; set; }


    }
}
