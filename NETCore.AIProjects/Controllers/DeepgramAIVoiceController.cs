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
            if(audioFile == null || audioFile.Length == 0)
            {
                ViewBag.Transcript = "Lütfen Bir ses dosyası seçiniz.";
                return View("Index");
            }

            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
                using(var content = new MultipartFormDataContent())
                {
                    var streamContent = new StreamContent(audioFile.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
                    content.Add(streamContent, "audio", audioFile.FileName);

                    var url = "https://api.deepgram.com/v1/listen?language=tr";
                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMsg = await response.Content.ReadAsStringAsync();
                        ViewBag.Transcript = "Transkripsiyon başarısız oldu! Hata: " + errorMsg;
                        return View("Index");
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(json);
                    var transcript = result?.results?.channels[0]?.alternatives[0]?.transcript;
                    ViewBag.Transcript = transcript ?? "Transkript Boş";
                }
        }
            return View("Index");
        }
}
}
