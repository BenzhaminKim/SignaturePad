using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;

namespace SignatureTest
{
   public class AzureOperations
    {

        public CloudBlobContainer _testContainer;
        public AzureOperations()
        {
            _testContainer = _blobClient.GetContainerReference("test");
        }
        CloudBlobClient _blobClient = CloudStorageAccount
            .Parse(connectionString)
            .CreateCloudBlobClient();

        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=dudgnl23;AccountKey=6DaGK5yjnzXL1C2bHwWydmBxR/AenC9nxRNEDmxruIfaOd1EQJv14Lr8ovn98k25QySnnvxfPSf72gcWGTUafw==;EndpointSuffix=core.windows.net";

        public async Task UploadFIleAsnyc(Stream stream)
        {
            var uniqueBlobName = Guid.NewGuid().ToString();
            uniqueBlobName += ".png";
            var todayDate = DateTime.Today.ToShortDateString();
            var directory = _testContainer.GetDirectoryReference(todayDate);
            var blobRef = directory.GetBlockBlobReference(uniqueBlobName);
            await blobRef.UploadFromStreamAsync(stream);
        }
    }
}
