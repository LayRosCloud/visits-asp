using Microsoft.AspNetCore.Mvc;

namespace LearnAsp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
