using Microsoft.AspNetCore.Mvc;
using NETCore.AIProjects.Services;

namespace NETCore.AIProjects.Controllers
{
    public class HuggingFaceSentimentAnalysisController : Controller
    {
        private readonly HuggingFaceSentimentAnalysisService _huggingFaceSentimentAnalysisService;

        public HuggingFaceSentimentAnalysisController(HuggingFaceSentimentAnalysisService huggingFaceSentimentAnalysisService)
        {
            _huggingFaceSentimentAnalysisService = huggingFaceSentimentAnalysisService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var sentiment = await _huggingFaceSentimentAnalysisService.SentimentAsync(text);
            ViewBag.Sentiment = sentiment;
            return View("Index");
        }
    }
}
