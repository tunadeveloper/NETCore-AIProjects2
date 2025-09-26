using Microsoft.AspNetCore.Mvc;

namespace NETCore.AIProjects.Controllers
{
    public class HuggingFaceSentimentAnalysis : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
