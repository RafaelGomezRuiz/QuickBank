using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Controllers
{
    public class PayController : Controller
    {
        private readonly IPayService payService;
        private readonly ISavingAccountService savingAccountService;
        private readonly IUserHelper userHelper; // NO ESTA EN USO

        public PayController(
            IPayService payService,
            ISavingAccountService savingAccountService,
            IUserHelper userHelper
        )
        {
            this.payService = payService;
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
            var epsvm = new ExpressPaySaveViewModel();
            epsvm.Accounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id);

            return View("MakeExpressPay", epsvm);
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPay(ExpressPaySaveViewModel epsvm)
        {
            //var aditionalValidations = ModelState.;

            //if (!modelWithAditionalErrors.IsValid) {
            //    epsvm.Accounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id);
            //    return View("MakeExpressPay", epsvm); 
            //}
            //var payStatus = await payService.MakeExpressPay(epsvm);

            return RedirectRoutesHelper.routeBasicHome;
        }
    }
}
