﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Helpers;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC")]
    public class PayController : Controller
    {
        private readonly IPayService payService;
        private readonly IPayValidationService payValidationService;
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly IUserHelper userHelper;
        private readonly IJsonHelper jsonHelper;

        public PayController(
            IPayService payService,
            IPayValidationService payValidationService,
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            IUserHelper userHelper,
            IJsonHelper jsonHelper
        )
        {
            this.payService = payService;
            this.payValidationService = payValidationService;
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.userHelper = userHelper;
            this.jsonHelper = jsonHelper;
        }

        public IActionResult PayOptions()
        {
            return View();
        }

        #region ExpressPay

        public async Task<IActionResult> ExpressPay()
        {
            // Fill the model wih data
            var epsvm = new ExpressPaySaveViewModel()
            {
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(userHelper.GetUser().Id)
            };

            return View("MakePay/MakeExpressPay", epsvm);
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPay(ExpressPaySaveViewModel epsvm)
        {
            // Validations before the payment
            ModelState.AddModelErrorRange(await payValidationService.ExpressPayValidation(epsvm));
            if (!ModelState.IsValid) return View("MakePay/MakeExpressPay", epsvm);

            // Store temporally the model
            TempData["ExpressPaySaveViewModel"] = jsonHelper.Serialize(epsvm);

            // Confirm the payment
            var confirmationModel = await payService.GetExpressPayConfirmation(epsvm.NumberAccountToPay);
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

        #endregion


        #region CreditCardPay

        public async Task<IActionResult> CreditCardPay()
        {
            // Fill the model wih data
            var user = userHelper.GetUser();
            var ccpsvm = new CreditCardPaySaveViewModel()
            {
                CreditCards = await creditCardService.GetAllByUserIdWithBalanceAsync(user.Id),
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(user.Id)
            };

            return View("MakePay/MakeCreditCardPay", ccpsvm);
        }

        [HttpPost]
        public async Task<IActionResult> CreditCardPay(CreditCardPaySaveViewModel ccpsvm)
        {
            // Validations before the payment
            ModelState.AddModelErrorRange(await payValidationService.CreditCardPayValidation(ccpsvm));
            if (!ModelState.IsValid) return View("MakePay/MakeCreditCardPay", ccpsvm);

            // Store temporally the model
            TempData["CreditCardPaySaveViewModel"] = jsonHelper.Serialize(ccpsvm);

            // Confirm the payment
            var confirmationModel = await payService.GetCreditCardPayConfirmation(ccpsvm.CreditCardIdToPay);
            return View("ConfirmPay", confirmationModel);
        }

        public async Task<IActionResult> ConfirmCreditCardPay()
        {
            // Get the model
            var ccpsvm = jsonHelper.Deserialize<CreditCardPaySaveViewModel>(TempData["CreditCardPaySaveViewModel"] as string);

            // Make the payment
            await payService.MakeCreditCardPay(ccpsvm);

            return View("PayConfirmed");
        }

        #endregion


    }
}
