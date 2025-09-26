using System.Net.Http.Headers;
using System.Text.Json;

namespace NETCore.AIProjects.Services
{
    public class HuggingFaceSummarizeTextService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "API_KEY";
        private readonly string huggingFaceUrl = "https://api-inference.huggingface.co/models/facebook/bart-large-cnn";

        public HuggingFaceSummarizeTextService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SummarizeAsync(string text)
        {
            var response = await _httpClient.PostAsync(
                huggingFaceUrl,
                new StringContent(JsonSerializer.Serialize(new { inputs = text }), System.Text.Encoding.UTF8, "application/json")
            );

            var json = await response.Content.ReadAsStringAsync();

            return json ?? $"Özet Yaparken Hata Oluştu: {json}";
        }
}
}
