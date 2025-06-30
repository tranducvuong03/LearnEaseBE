using Google.Cloud.Storage.V1;

public class GcsUploader
{
    private readonly string _bucketName = "learnease";

    public async Task<string> UploadMp3Async(byte[] audioBytes, string originalFileName)
    {
        var credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
        if (string.IsNullOrEmpty(credentialsPath) || !File.Exists(credentialsPath))
        {
            Console.WriteLine("❌ GOOGLE_APPLICATION_CREDENTIALS is missing or file not found.");
            throw new InvalidOperationException("Credentials file not found.");
        }
        Console.WriteLine($"📂 Using credentials from: {credentialsPath}");

        try
        {
            var storage = await StorageClient.CreateAsync();
            using var stream = new MemoryStream(audioBytes);

            string fileName = $"{Guid.NewGuid()}_{originalFileName}";

            Console.WriteLine($"⏫ Uploading {fileName} to bucket {_bucketName}...");
            await storage.UploadObjectAsync(_bucketName, fileName, "audio/mpeg", stream);

            Console.WriteLine("✅ Upload successful");

            // Nếu bucket đã bật public access, link này có thể dùng được
            return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Upload failed: {ex.Message}");
            throw;
        }
    }
}
