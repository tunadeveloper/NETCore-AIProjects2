using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.AIProjects.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NETCore.AIProjects.Controllers
{
    public class DeepgramAIVoiceController : Controller
    {
        private readonly DeepgramAIVoiceService _deepgramAIVoiceService;

        public DeepgramAIVoiceController(DeepgramAIVoiceService deepgramAIVoiceService)
        {
            _deepgramAIVoiceService = deepgramAIVoiceService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile audioFile)
        {
            var transcript = await _deepgramAIVoiceService.TranscribeAsync(audioFile);
            ViewBag.Transcript = transcript;
            return View("Index");
        }
    }
}
