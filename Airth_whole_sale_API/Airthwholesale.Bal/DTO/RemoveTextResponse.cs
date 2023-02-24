using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class RemoveTextResponse
    {
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
        public string Removetext_count { get; set; }

        public string Image_id { get; set; }

        public string OriginalFilepath_fordisplay_basesixtyfour { get; set; }

        public string RemoveFilepath_fordisplay_basesixtyfour { get; set; }
        public int? sortorder { get; set; }
    }
}
