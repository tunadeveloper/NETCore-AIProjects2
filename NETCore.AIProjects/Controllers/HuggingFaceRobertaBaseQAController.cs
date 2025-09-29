using Microsoft.AspNetCore.Mvc;
using NETCore.AIProjects.Services;

namespace NETCore.AIProjects.Controllers
{
    public class HuggingFaceRobertaBaseQAController : Controller
    {
        private readonly HuggingFaceRobertaBaseQAService _huggingFaceRobertaBaseQAService;

        public HuggingFaceRobertaBaseQAController(HuggingFaceRobertaBaseQAService huggingFaceRobertaBaseQAService)
        {
            _huggingFaceRobertaBaseQAService = huggingFaceRobertaBaseQAService;
        }

        public IActionResult Index()=>View();

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Index(string question, string context)
        {
            var result = await _huggingFaceRobertaBaseQAService.RobertaBaseQA(question, context);
            ViewBag.Result = result;
            return View();
        }

    }
}
