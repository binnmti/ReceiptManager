using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

public class FormRecognizerService
{
    private readonly DocumentAnalysisClient _dccumentAnalysisClient;


    public FormRecognizerService(IConfiguration configuration)
    {
        var endpoint = configuration["FormRecognizer:Endpoint"];
        var apiKey = configuration["FormRecognizer:ApiKey"];
        _dccumentAnalysisClient = new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
    }

    public async Task<string> RecognizeImageAsync(string blobName)
    {
        var uri = new Uri($"https://receiptmanagerstoreage.blob.core.windows.net/receipt/{blobName}");
        var operation = await _dccumentAnalysisClient.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-layout", uri);
        var result = await operation.WaitForCompletionAsync();
        // 画像認識結果の処理
        return result.Value.ToString();
    }
}
