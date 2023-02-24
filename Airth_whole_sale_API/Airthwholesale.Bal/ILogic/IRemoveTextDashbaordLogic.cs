using Airthwholesale.Bal.DTO;
using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface IRemoveTextDashbaordLogic
    {

        DashboardFilesDetailsViewModelDTO GetRemovtextDashboard(string opcode);

        
        List<CleanImagesDetailsDTO> GetCleanImagesDetails(string opcode);

        List<JDPListOfPhotos> GetimagesForClean(string opcode);
    }
}
