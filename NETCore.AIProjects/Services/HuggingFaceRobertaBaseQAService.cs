using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NETCore.AIProjects.Services
{
    public class HuggingFaceRobertaBaseQAService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiUrl = "https://api-inference.huggingface.co/models/deepset/roberta-base-squad2";
        private readonly string apiKey = "";

        public HuggingFaceRobertaBaseQAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> RobertaBaseQA(string question, string context)
        {
            var requestBody = new
            {
                inputs = new
                {
                    question = question,
                    context = context
                }
            };

            var response = await _httpClient.PostAsync(
                apiUrl,
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            if (doc.RootElement.TryGetProperty("answer", out var answer))
            {
                return answer.GetString();
            }

            return "Cevap bulunamadı";
        }

    }
}
