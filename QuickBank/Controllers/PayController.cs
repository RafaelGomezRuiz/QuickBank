using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayService payService;

        public PayController(IPayService payService)
        {
            this.payService = payService;
        }

        public IActionResult ExpressPay()
        {
            return View(new ExpressPaySaveViewModel());
        }

        [HttpPost]
        public IActionResult ExpressPay(ExpressPaySaveViewModel epsvm)
        {
            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
