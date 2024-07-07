using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Services.Products
{
    public class PayValidationService : IPayValidationService
    {
        private readonly ISavingAccountService savingAccountService;

        public PayValidationService(ISavingAccountService savingAccountService)
        {
            this.savingAccountService = savingAccountService;
        }

        public async Task<Dictionary<string, string>> ExpressPayValidation(ExpressPaySaveViewModel epsvm)
        {
            // Resouces
            var errors = new Dictionary<string, string>();
            var savingAccountToPay = await savingAccountService.GetViewModelByNumberAccountAsync(epsvm.NumberAccountToPay);
            var savingAccountFromPay = await savingAccountService.GetByIdAsync(epsvm.AccountIdFromPay);

            #region Amount

            // Conditionals
            bool amountIsNull = epsvm.Amount == null;
            bool amountIsValid = epsvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;

            if (amountIsNull) errors.Add("InvalidAmountNull", "The amount field cannot be empty");
            else if (!amountIsValid) errors.Add("InvalidAmount", $"You must enter a valid amount, ${BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");

            #endregion

            #region Number_Account

            // Conditionals
            bool numberAccountIsNull = string.IsNullOrEmpty(epsvm.NumberAccountToPay);
            bool numberAccountCharactersIsValid = !numberAccountIsNull && epsvm.NumberAccountToPay.Length == BusinessLogicConstantsHelper.MaxLengthNumberAccount;
            bool savingAccountToPayExists = savingAccountToPay != null;
            bool savingAccountToPayIsValid = savingAccountToPayExists && savingAccountToPay.Status == (int)EProductStatus.ACTIVE;

            if (numberAccountIsNull) errors.Add("InvalidNumberAccountNull", "The number account field cannot be empty");
            else if (!numberAccountCharactersIsValid) errors.Add("InvalidNumberCharacters", $"The number of characters is {BusinessLogicConstantsHelper.MaxLengthNumberAccount} minimun");
            else if (!savingAccountToPayExists) errors.Add("InvalidNumberAccount", $"The account number is not valid");
            else if (!savingAccountToPayIsValid) errors.Add("InvalidSavingAccount", $"This account is not available to deposit");

            #endregion

            #region Saving_Account_Id_From_Pay

            //Conditionals
            bool savingAccountFromPayIsNull = epsvm.AccountIdFromPay == 0;
            bool savingAccountFromPayHasEnoughMoney = !savingAccountFromPayIsNull && savingAccountFromPay.Balance >= epsvm.Amount;

            if (savingAccountFromPayIsNull) errors.Add("InvalidSavingAccountFromPayNull", "Select a valid option");
            else if (!savingAccountFromPayHasEnoughMoney) errors.Add("InvalidBalance", $"The account to be debited does not have sufficient balance");

            #endregion

            return errors;
        }
    }
}
