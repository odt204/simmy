using Microsoft.AspNetCore.Mvc;

namespace SimmyWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
