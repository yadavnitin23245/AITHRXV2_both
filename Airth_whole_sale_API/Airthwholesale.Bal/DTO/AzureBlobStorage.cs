using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.DTO
{
    public class AzureBlobStorage
    {
        string storageConnectionString = string.Empty;
        string azurecontainername = string.Empty;

        public AzureBlobStorage(IOptions<RemovetextDTOConfig> settings)
        {
            this.storageConnectionString = settings.Value.AzurestorageConnectionString;
            this.azurecontainername = settings.Value.AzureContainerName;
            // this.storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=removetextfromfile;AccountKey=2rlKVdbtLABCP4pfnnmib0qf0AtCUfPvTSmSF3whypNiDVzNnazmW5igepzZtqrjhmTCxLCN5oOW+AStp/WPhQ==;EndpointSuffix=core.windows.net";
        }

        public async Task<string> Create(Stream stream, string path)
        {
            // Initialise client in a different place if you like
            //string storageConnectionString = "DefaultEndpointsProtocol=https;"
            //            + "AccountName=[ACCOUNT]"
            //            + ";AccountKey=[KEY]"
            //            + ";EndpointSuffix=core.windows.net";

            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
                var blobClient = account.CreateCloudBlobClient();


                // Make sure container is there
                var blobContainer = blobClient.GetContainerReference(this.azurecontainername);
                await blobContainer.CreateIfNotExistsAsync();

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(path);
                await blockBlob.UploadFromStreamAsync(stream);

                var filepath = blockBlob.Uri.LocalPath;
                return filepath;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
