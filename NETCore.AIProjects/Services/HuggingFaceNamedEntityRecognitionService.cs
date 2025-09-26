using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NETCore.AIProjects.Services
{
    public class HuggingFaceNamedEntityRecognitionService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "API_KEY";
        private readonly string apiUrl = "https://api-inference.huggingface.co/models/dslim/bert-base-NER";

        public HuggingFaceNamedEntityRecognitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> EntityRecognition(string text)
        {
            var requestBody = new
            {
                inputs = text
            };

            var response = await _httpClient.PostAsync(apiUrl, new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));
            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                var entity = item.GetProperty("entity_group").GetString();
                var word = item.GetProperty("word").GetString();
                double score = Math.Round(item.GetProperty("score").GetDouble() * 100, 2);
                return $"Entity: {entity}, Word: {word}, Score: {score}";
            }
            return $"Entity Tanımlanamadı: {json}";
        }
    }
}
