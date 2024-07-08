using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdministrationUserController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}
