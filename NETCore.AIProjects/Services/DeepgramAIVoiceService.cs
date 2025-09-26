using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NETCore.AIProjects.Services
{
    public class DeepgramAIVoiceService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "API_KEY";
        private readonly string deepgramUrl = "https://api.deepgram.com/v1/listen?language=tr";

        public DeepgramAIVoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Token", apiKey);
        }

        public async Task<string> TranscribeAsync(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return "Lütfen bir ses dosyası seçiniz.";

            using var stream = audioFile.OpenReadStream();
            using var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(audioFile.ContentType);

            var response = await _httpClient.PostAsync(deepgramUrl, content);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return "Transkripsiyon başarısız oldu! Hata: " + json;

            var result = JsonConvert.DeserializeObject<dynamic>(json);
            return result?.results?.channels[0]?.alternatives[0]?.transcript ?? "Transkript boş";
        }
    }
}
