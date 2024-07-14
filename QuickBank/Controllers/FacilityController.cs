using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;
using QuickBank.Helpers;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC")]
    public class FacilityController : Controller
    {
        private readonly IFacilityService facilityService;
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly IFacilityValidationService facilityValidationService;
        private readonly IUserHelper userHelper;

        public FacilityController(
            IFacilityService facilityService,
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            IFacilityValidationService facilityValidationService,
            IUserHelper userHelper
        )
        {
            this.facilityService = facilityService;
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.facilityValidationService = facilityValidationService;
            this.userHelper = userHelper;
        }


        #region Transfer

        public async Task<IActionResult> Transfer()
        {
            var tsvm = new TransferSaveViewModel
            {
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id),
            };

            return View("MakeTransfer", tsvm);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferSaveViewModel tsvm)
        {
            // Validations before transfer
            ModelState.AddModelErrorRange(await facilityValidationService.TransferValidation(tsvm));
            if (!ModelState.IsValid) return View("MakeTransfer", tsvm);

            //Make transfer
            await facilityService.MakeTransfer(tsvm);

            return View("TransactionConfirmed");
        }

        #endregion


        #region CashAdvance

        public async Task<IActionResult> CashAdvance()
        {
            // Fill the model wih data
            var user = userHelper.GetUser();
            var casvm = new CashAdvancesSaveViewModel()
            {
                CreditCards = await creditCardService.GetAllByUserIdAsync(user.Id),
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(user.Id)
            };

            return View("MakeCashAdvance", casvm);
        }

        [HttpPost]
        public async Task<IActionResult> CashAdvance(CashAdvancesSaveViewModel casvm)
        {
            // Validations before the advance
            ModelState.AddModelErrorRange(await facilityValidationService.CashAdvanceValidation(casvm));
            if (!ModelState.IsValid) return View("MakeCashAdvance", casvm);

            // Make the cash advance
            await facilityService.MakeCashAdvance(casvm);

            return View("TransactionConfirmed");
        }

        #endregion

    }
}
