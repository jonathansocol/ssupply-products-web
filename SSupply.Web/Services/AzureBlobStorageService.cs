using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SSupply.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Services
{
    public class AzureBlobStorageService : IImageStorageService
    {
        private readonly CloudBlobContainer _container;

        public AzureBlobStorageService(string connectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference(containerName);
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var blobName = Guid.NewGuid().ToString();
            var blob = _container.GetBlockBlobReference(blobName);

            using (var fileStream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(fileStream);
            }

            return blob.Uri.OriginalString;
        }

        public async Task DeleteFile(string url)
        {
            var uri = new Uri(url);
            var blob = _container.GetBlockBlobReference(new CloudBlockBlob(uri).Name);

            await blob.DeleteIfExistsAsync();
        }
    }
}
