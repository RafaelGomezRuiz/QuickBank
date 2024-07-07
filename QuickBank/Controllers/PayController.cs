using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.Facilities;

namespace QuickBank.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayService payService;
        private readonly IPayValidationService payValidationService;
        private readonly ISavingAccountService savingAccountService;
        private readonly IUserHelper userHelper;

        public PayController(
            IPayService payService,
            IPayValidationService payValidationService,
            ISavingAccountService savingAccountService,
            IUserHelper userHelper
        )
        {
            this.payService = payService;
            this.payValidationService = payValidationService;
            this.savingAccountService = savingAccountService;
            this.userHelper = userHelper;
        }

        public IActionResult PayOptions()
        {
            return View();
        }

        public async Task<IActionResult> ExpressPay()
        {
            // Fill the model wih data
            var epsvm = new ExpressPaySaveViewModel()
            {
                Accounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id)
            };

            return View("MakeExpressPay", epsvm);
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPay(ExpressPaySaveViewModel epsvm)
        {
            // Validations before the payment
            ModelState.AddModelErrorRange(await payValidationService.ExpressPayValidation(epsvm));
            if (!ModelState.IsValid) return View("MakeExpressPay", epsvm);

            await payService.MakeExpressPay(epsvm);

            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
