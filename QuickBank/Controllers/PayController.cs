using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayService payService;
        private readonly IUserHelper userHelper;

        public PayController(IPayService payService)
        {
            this.payService = payService;
        }

        public IActionResult PayOptions()
        {
            return View();
        }

        public IActionResult ExpressPay()
        {
            return View("MakeExpressPay", new ExpressPaySaveViewModel());
        }

        [HttpPost]
        public IActionResult ExpressPay(ExpressPaySaveViewModel epsvm)
        {
            if (!ModelState.IsValid) return View(epsvm);

            // Things

            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
