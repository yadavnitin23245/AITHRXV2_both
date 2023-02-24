using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class RemovetextDTOConfig
    {
        public string ApplicationName { get; set; }
        public int Version { get; set; }
        public string BaseAddress { get; set; }

        public string originpath { get; set; }
        public string saveimagepath { get; set; }
        public string callaspnetapi { get; set; }

        public string AzurestorageConnectionString { get; set; }
        public string AzureContainerName { get; set; }

        public string pythonProjectpath { get; set; }

        public string azureblob_cdnlink { get; set; }

        public string PythonAPIurl { get; set; }
    }
}
