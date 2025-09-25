using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NETCore.AIProjects.Controllers
{
    public class DeepgramAIVoiceController : Controller
    {
        private readonly string apiKey = "API_KEY";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                ViewBag.Transcript = "Lütfen bir ses dosyası seçiniz.";
                return View("Index");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Token", apiKey);

                using (var stream = audioFile.OpenReadStream())
                using (var content = new StreamContent(stream))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(audioFile.ContentType);

                    var url = "https://api.deepgram.com/v1/listen?language=tr";
                    var response = await client.PostAsync(url, content);

                    var json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Transcript = "Transkripsiyon başarısız oldu! Hata: " + json;
                        return View("Index");
                    }

                    var result = JsonConvert.DeserializeObject<dynamic>(json);
                    var transcript = result?.results?.channels[0]?.alternatives[0]?.transcript;
                    ViewBag.Transcript = transcript ?? "Transkript boş";
                }
            }

            return View("Index");
        }
    }
}
