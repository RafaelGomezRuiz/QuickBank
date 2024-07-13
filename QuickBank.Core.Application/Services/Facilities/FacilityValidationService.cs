﻿using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class FacilityValidationService : IFacilityValidationService
    {
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly IBeneficeService beneficeService;
        private readonly IUserHelper userHelper;

        public FacilityValidationService(
            ISavingAccountService savingAccountService,
            IBeneficeService beneficeService,
            IUserHelper userHelper,
            ICreditCardService creditCardService
        )
        {
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.beneficeService = beneficeService;
            this.userHelper = userHelper;
        }

        public async Task<Dictionary<string, string>> TransferValidation(TransferSaveViewModel tsvm)
        {
            var errors = new Dictionary<string, string>();
            var originAccount = await savingAccountService.GetByIdAsync(tsvm.SavingAccountOriginId);

            #region Amount_Validation

            bool amountIsNull = tsvm.Amount == 0.0;
            bool amountIsValid = tsvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;

            if (amountIsNull) errors.Add("InvalidAmountNull", "The amount field cannot be $0.0 or empty");
            else if (!amountIsValid) errors.Add("InvalidAmount", $"You must enter a valid amount, ${BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");

            #endregion

            #region Account_Destiny

            // Coditionals
            bool destinyAccountIsValidOption = tsvm.SavingAccountDestinyId != 0;
            bool sameAccount = tsvm.SavingAccountOriginId == tsvm.SavingAccountDestinyId;

            if (!destinyAccountIsValidOption) errors.Add("InvalidDestinyAccountSelection", "Please select a valid destination account.");
            else if (sameAccount) errors.Add("InvalidSameAccount", "The origin and destination accounts must be different.");

            #endregion

            #region Account_Origin

            // Conditionals
            bool originAccountIsValidOption = tsvm.SavingAccountOriginId != 0;
            bool originAccountHasEnoughMoney = originAccountIsValidOption && originAccount.Balance >= tsvm.Amount;

            if (!originAccountIsValidOption) errors.Add("InvalidOriginAccountSelection", "Please select a valid origin account.");
            else if (amountIsValid && destinyAccountIsValidOption && !originAccountHasEnoughMoney) errors.Add("InsufficientFunds", "The account to be debited does not have sufficient balance");

            #endregion

            return errors;
        }

        public async Task<Dictionary<string, string>> CashAdvanceValidation(CashAdvancesSaveViewModel casvm)
        {
            // Resouces
            var errors = new Dictionary<string, string>();
            var creditCardFromPay = await creditCardService.GetByIdAsync(casvm.CreditCardId);

            #region Amount

            // Conditions
            bool amountIsNull = casvm.Amount == 0.0;
            bool amountIsValid = casvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;

            if (amountIsNull) errors.Add("InvalidAmountNull", "The amount field cannot be $0.0 or empty");
            else if (!amountIsValid) errors.Add("InvalidAmount", $"You must enter a valid amount, ${BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");

            #endregion

            #region Credit_Card_Id_From_Pay

            // Conditions
            bool creditCardIdFromPayIsValidOption = casvm.CreditCardId != 0;
            bool creditCardFromPayHasEnoughBalance = creditCardFromPay != null && casvm.Amount <= creditCardFromPay.LimitCredit - creditCardFromPay.Balance;

            if (!creditCardIdFromPayIsValidOption) errors.Add("InvalidCreditCardIdOption", "Select a valid option");
            else if (amountIsValid && !creditCardFromPayHasEnoughBalance) errors.Add("InvalidBalance", "The credit card to be debited does not have sufficient balance");

            #endregion

            #region Saving_Account_Id_From_Pay

            // Conditions
            bool savingAccountIdToPayPayIsValidOption = casvm.SavingAccountId != 0;

            if (!savingAccountIdToPayPayIsValidOption) errors.Add("InvalidSavingAccountIdOption", "Select a valid option");

            #endregion

            return errors;
        }

        public async Task<Dictionary<string, string>> ValidateBeneficiary(BeneficeSaveViewModel model)
        {
            var errors = new Dictionary<string, string>();

            var user = userHelper.GetUser();
            var savingAccount = await savingAccountService.GetViewModelByNumberAccountAsync(model.NumberAccount);
            var userBeneficiaries = await beneficeService.GetBeneficiariesWithFullNameAsync();

            #region Beneficiary

            // Validación de que la cuenta es válida
            bool accountFieldIsValid = !string.IsNullOrWhiteSpace(model.NumberAccount);
            bool accountIsValid = savingAccount != null;
            bool ownAccount = accountIsValid && savingAccount.UserId == user.Id;
            bool duplicateBeneficiary = userBeneficiaries.Any(b => b.BenefitedSavingAccount.AccountNumber == model.NumberAccount);

            if (!accountFieldIsValid) errors.Add("InvalidNumberAccount", "The account number field cannot be empty.");
            else if (!accountIsValid) errors.Add("InvalidAccount", "The account number is not valid.");
            else if (ownAccount) errors.Add("OwnAccount", "You cannot add your own account as a beneficiary.");
            else if (duplicateBeneficiary) errors.Add("DuplicateBeneficiary", "This beneficiary is already added.");

            #endregion

            return errors;
        }
    }
}