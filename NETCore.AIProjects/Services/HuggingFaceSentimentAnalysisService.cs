using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NETCore.AIProjects.Services
{
    public class HuggingFaceSentimentAnalysisService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "API_KEY";
        private readonly string huggingFaceUrl = "https://api-inference.huggingface.co/models/cardiffnlp/twitter-roberta-base-sentiment";

        public HuggingFaceSentimentAnalysisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SentimentAsync(string text)
        {
            var response = await _httpClient.PostAsync(
                huggingFaceUrl,
                new StringContent(JsonSerializer.Serialize(new { inputs = text }), Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var predictions = JsonSerializer.Deserialize<List<List<Prediction>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var topLabel = predictions?.FirstOrDefault()?.OrderByDescending(p => p.Score).FirstOrDefault();
            if (topLabel is null) return "Bilinmeyen";

            return topLabel.Label switch
            {
                "LABEL_0" => $"NEGATİF 😞 (%{topLabel.Score * 100:F2})",
                "LABEL_1" => $"NÖTR 😐 (%{topLabel.Score * 100:F2})",
                "LABEL_2" => $"POZİTİF 😄 (%{topLabel.Score * 100:F2})",
                _ => "BİLİNMİYOR"
            };
        }

        private record Prediction(
            [property: JsonPropertyName("label")] string Label,
            [property: JsonPropertyName("score")] double Score
        );
    }
}
