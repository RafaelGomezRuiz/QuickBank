﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Logs;
using QuickBank.Helpers;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC, ADMIN")]
    public class FacilityController : Controller
    {
        private readonly IFacilityService facilityService;
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly IFacilityValidationService facilityValidationService;
        private readonly IUserHelper userHelper;
        private readonly ILogService logService;

        public FacilityController(
            IFacilityService facilityService,
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            IFacilityValidationService facilityValidationService,
            IUserHelper userHelper,
            ILogService logService)
        {
            this.facilityService = facilityService;
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.facilityValidationService = facilityValidationService;
            this.userHelper = userHelper;
            this.logService = logService;
        }


        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> Transfer()
        {
            var tsvm = new TransferSaveViewModel
            {
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id),
            };

            return View("MakeTransfer", tsvm);
        }

        [HttpPost]
        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> Transfer(TransferSaveViewModel tsvm)
        {
            // Validations before transfer
            ModelState.AddModelErrorRange(await facilityValidationService.TransferValidation(tsvm));
            if (!ModelState.IsValid) return View("MakeTransfer", tsvm);

            //Make transfer
            await facilityService.MakeTransfer(tsvm);

            return View("TransactionConfirmed");
        }


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
    }
}
