using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class FacilityValidationService : IFacilityValidationService
    {
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;

        public FacilityValidationService(
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService
        )
        {
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
        }

        public async Task<Dictionary<string, string>> ValidateTransfer(TransferSaveViewModel model)
        {
            var errors = new Dictionary<string, string>();

            var originAccount = await savingAccountService.GetByIdAsync(model.SavingAccountOriginId);
            var destinyAccount = await savingAccountService.GetByIdAsync(model.SavingAccountDestinyId);

            #region Account_Validation

            bool sameAccount = model.SavingAccountOriginId == model.SavingAccountDestinyId;
            bool originAccountExists = originAccount != null;
            bool destinyAccountExists = destinyAccount != null;

            if (model.SavingAccountOriginId == 0) errors.Add("InvalidOriginAccountSelection", "Please select a valid origin account.");
            else if (!originAccountExists) errors.Add("InvalidOriginAccount", "The origin account is not valid.");
            else if (model.SavingAccountDestinyId == 0) errors.Add("InvalidDestinyAccountSelection", "Please select a valid destination account.");
            else if (!destinyAccountExists) errors.Add("InvalidDestinyAccount", "The destination account is not valid.");
            else if (sameAccount) errors.Add("InvalidSameAccount", "The origin and destination accounts must be different.");

            #endregion

            #region Amount_Validation

            bool amountIsPositive = model.Amount > 0;
            bool amountIsAboveMinimum = model.Amount >= 100;
            bool originAccountHasEnoughBalance = originAccountExists && originAccount.Balance >= model.Amount;

            if (!amountIsPositive) errors.Add("InvalidAmount", "The amount must be greater than zero.");
            else if (!amountIsAboveMinimum) errors.Add("AmountBelowMinimum", "The amount must be greater than 100.");
            else if (amountIsAboveMinimum && !originAccountHasEnoughBalance) errors.Add("InsufficientFunds", "The origin account does not have sufficient balance.");

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
    }
}