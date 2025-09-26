using Microsoft.AspNetCore.Mvc;
using NETCore.AIProjects.Services;

namespace NETCore.AIProjects.Controllers
{
    public class HuggingFaceNamedEntityRecognitionController : Controller
    {
        private readonly HuggingFaceNamedEntityRecognitionService _huggingFaceNamedEntityRecognitionService;

        public HuggingFaceNamedEntityRecognitionController(HuggingFaceNamedEntityRecognitionService huggingFaceNamedEntityRecognitionService)
        {
            _huggingFaceNamedEntityRecognitionService = huggingFaceNamedEntityRecognitionService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string inputText)
        {
            var result = await _huggingFaceNamedEntityRecognitionService.EntityRecognition(inputText);
            ViewBag.Result = result;
            return View();
        }
    }
}
