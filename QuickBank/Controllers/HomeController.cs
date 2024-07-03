using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
