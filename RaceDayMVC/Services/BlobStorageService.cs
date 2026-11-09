using Azure.Storage.Blobs;

namespace RaceDayMVC.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            string connectionString =
                configuration["AzureBlobStorage:ConnectionString"];

            string containerName =
                configuration["AzureBlobStorage:ContainerName"];

            BlobServiceClient blobServiceClient =
                new BlobServiceClient(connectionString);

            _containerClient =
                blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string fileName =
                Guid.NewGuid() +
                Path.GetExtension(file.FileName);

            BlobClient blobClient =
                _containerClient.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
    }
}
