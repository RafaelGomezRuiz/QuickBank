using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC, ADMIN")]
    public class HomeController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult BasicHome()
        {
            return View();
        }
    }
}
