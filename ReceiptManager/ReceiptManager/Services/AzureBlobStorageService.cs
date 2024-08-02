using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

public class AzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
    }

    public async Task UploadFileAsync(string fileName, Stream fileStream)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("your-container-name");
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);
    }
}