using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure;
using Microsoft.Extensions.Configuration;

public class FormRecognizerService
{
    private readonly FormRecognizerClient _formRecognizerClient;

    public FormRecognizerService(IConfiguration configuration)
    {
        var endpoint = configuration["FormRecognizer:Endpoint"];
        var apiKey = configuration["FormRecognizer:ApiKey"];
        _formRecognizerClient = new FormRecognizerClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
    }

    public async Task<string> RecognizeImageAsync(string blobName)
    {
        var uri = new Uri($"https://your-storage-account-name.blob.core.windows.net/your-container-name/{blobName}");
        var operation = await _formRecognizerClient.StartRecognizeContentFromUriAsync(uri);
        var result = await operation.WaitForCompletionAsync();
        // 画像認識結果の処理
        return result.Value.ToString();
    }
}
