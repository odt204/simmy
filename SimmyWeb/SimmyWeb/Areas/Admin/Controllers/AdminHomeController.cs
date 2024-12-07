using Microsoft.AspNetCore.Mvc;
using SimmyWeb.Data;

namespace SimmyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly SimmyWebContext _context;
        public AdminHomeController(SimmyWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
