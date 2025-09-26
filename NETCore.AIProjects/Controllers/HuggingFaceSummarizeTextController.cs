using Microsoft.AspNetCore.Mvc;
using NETCore.AIProjects.Services;

namespace NETCore.AIProjects.Controllers
{
    public class HuggingFaceSummarizeTextController : Controller
    {
        private readonly HuggingFaceSummarizeTextService _huggingFaceSummarizeTextService;

        public HuggingFaceSummarizeTextController(HuggingFaceSummarizeTextService huggingFaceSummarizeTextService)
        {
            _huggingFaceSummarizeTextService = huggingFaceSummarizeTextService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var summarize = await _huggingFaceSummarizeTextService.SummarizeAsync(text);
            ViewBag.Summarize = summarize;
            return View("Index");
        }

    }
}
