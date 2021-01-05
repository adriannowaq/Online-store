using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineStore.Config;
using OnlineStore.Infrastructure.Services.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Services
{
    public class GoogleCloudStorageService : ICloudStorageService
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string storageBucket;

        public GoogleCloudStorageService(IOptions<GoogleCloudSettings> googleCloudSettings)
        {
            this.googleCredential = GoogleCredential.FromJson(googleCloudSettings.Value.CredentialJson);
            this.storageClient = StorageClient.Create(googleCredential);
            this.storageBucket = googleCloudSettings.Value.StorageBucket;
        }

        public async Task<UploadFileModel> UploadFileAsync(IFormFile file)
        {
            var fileName = $"{file.FileName}-{Guid.NewGuid()}";

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var dataObject = await storageClient
                .UploadObjectAsync(storageBucket, fileName, file.ContentType, memoryStream);

            return new UploadFileModel()
            {
                FileUrl = dataObject.MediaLink,
                FileName = fileName
            };
        }

        public Task DeleteFileAsync(string fileName)
        {
            return storageClient.DeleteObjectAsync(storageBucket, fileName);
        }
    }
}
