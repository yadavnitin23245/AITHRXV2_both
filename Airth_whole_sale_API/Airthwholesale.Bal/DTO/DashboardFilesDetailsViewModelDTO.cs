using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class DashboardFilesDetailsViewModelDTO
    {
        public DashboardFilesDetailsViewModelDTO()
        {

            RemoveTextFiledetailsDTO = new List<RemoveTextFiledetailsDTO>();
        }
        public int TotalFilescount { get; set; }

        public int TotalRemovedTextfilecount { get; set; }

        public int TotalRemovedTextfilecount_Percentage { get; set; }

        public int TotalfileUploaded_azurePercentage { get; set; }

        public int Total_file_uploadedToazure { get; set; }

        public List<RemoveTextFiledetailsDTO> RemoveTextFiledetailsDTO { get; set; }

    }
}
