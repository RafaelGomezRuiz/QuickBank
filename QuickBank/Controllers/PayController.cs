using Microsoft.AspNetCore.Mvc;
using QuickBank.Helpers;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using Microsoft.AspNetCore.Authorization;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC")]
    public class PayController : Controller
    {
        private readonly IPayService payService;
        private readonly IPayValidationService payValidationService;
        private readonly ISavingAccountService savingAccountService;
        private readonly IUserHelper userHelper;
        private readonly IJsonHelper jsonHelper;

        public PayController(
            IPayService payService,
            IPayValidationService payValidationService,
            ISavingAccountService savingAccountService,
            IUserHelper userHelper,
            IJsonHelper jsonHelper
        )
        {
            this.payService = payService;
            this.payValidationService = payValidationService;
            this.savingAccountService = savingAccountService;
            this.userHelper = userHelper;
            this.jsonHelper = jsonHelper;
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

            // Store temporally the model
            TempData["ExpressPaySaveViewModel"] = jsonHelper.Serialize(epsvm);

            // Confirm the payment
            var confirmationModel = await payService.GetPayConfirmation(epsvm.NumberAccountToPay, "ConfirmExpressPay");
            return View("ConfirmPay", confirmationModel);
        }

        public async Task<IActionResult> ConfirmExpressPay()
        {
            // Get the model
            var epsvm = jsonHelper.Deserialize<ExpressPaySaveViewModel>(TempData["ExpressPaySaveViewModel"] as string);

            // Make the payment
            await payService.MakeExpressPay(epsvm);

            return View("PayConfirmed");
        }
    }
}
