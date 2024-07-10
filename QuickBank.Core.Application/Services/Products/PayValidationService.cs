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
            bool amountIsNull = epsvm.Amount == 0.0;
            bool amountIsValid = epsvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;

            if (amountIsNull) errors.Add("InvalidAmountNull", "The amount field cannot be $0.0 or empty");
            else if (!amountIsValid) errors.Add("InvalidAmount", $"You must enter a valid amount, ${BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");

            #endregion

            #region Number_Account

            // Conditionals
            bool numberAccountIsNull = string.IsNullOrEmpty(epsvm.NumberAccountToPay);
            bool numberAccountCharactersIsValid = !numberAccountIsNull && epsvm.NumberAccountToPay.Length == BusinessLogicConstantsHelper.MaxLengthNumberAccount;
            bool savingAccountToPayExists = savingAccountToPay != null;
            bool savingAccountFromPayExists = savingAccountFromPay != null;
            bool savingAccountToPayIsValid = savingAccountToPayExists && savingAccountToPay.Status == (int)EProductStatus.ACTIVE;
            bool savingAccountToPayIsSameFromPay = savingAccountToPayExists && savingAccountFromPayExists &&  savingAccountToPay.AccountNumber == savingAccountFromPay.AccountNumber;

            if (numberAccountIsNull) errors.Add("InvalidNumberAccountNull", "The number account field cannot be empty");
            else if (!numberAccountCharactersIsValid) errors.Add("InvalidNumberCharacters", $"The number of characters is {BusinessLogicConstantsHelper.MaxLengthNumberAccount} minimun");
            else if (!savingAccountToPayExists) errors.Add("InvalidNumberAccount", "The account number is not valid");
            else if (!savingAccountToPayIsValid) errors.Add("InvalidSavingAccount", "This account is not available to deposit");
            else if (savingAccountToPayIsSameFromPay) errors.Add("InvalidSameSavingAccount", "You cannot pay to the same account that you are going to withdraw");

            #endregion

            #region Saving_Account_Id_From_Pay

            //Conditionals
            bool savingAccountFromPayIsValidOption = epsvm.AccountIdFromPay != 0;
            bool savingAccountFromPayHasEnoughMoney = savingAccountFromPayIsValidOption && savingAccountFromPay.Balance >= epsvm.Amount;

            if (!savingAccountFromPayIsValidOption) errors.Add("InvalidSavingAccountFromPayOption", "Select a valid option");
            else if (amountIsValid && savingAccountToPayIsValid && !savingAccountFromPayHasEnoughMoney) errors.Add("InvalidBalance", "The account to be debited does not have sufficient balance");

            #endregion

            return errors;
        }


        public async Task<Dictionary<string, string>> CreditCardPayValidation(CreditCardPaySaveViewModel ccpsvm)
        {
            // Resouces
            var errors = new Dictionary<string, string>();
            var savingAccountFromPay = await savingAccountService.GetByIdAsync(ccpsvm.SavingAccountIdFromPay);

            #region Amount

            // Conditions
            bool amountIsNull = ccpsvm.Amount == 0.0;
            bool amountIsValid = ccpsvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;

            if (amountIsNull) errors.Add("InvalidAmountNull", "The amount field cannot be $0.0 or empty");
            else if (!amountIsValid) errors.Add("InvalidAmount", $"You must enter a valid amount, ${BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");

            #endregion

            #region Credit_Card_Id_To_Pay

            // Conditions
            bool creditCardIdToPayIsValidOption = ccpsvm.CreditCardIdToPay != 0;

            if (!creditCardIdToPayIsValidOption) errors.Add("InvalidCreditCardIdOption", "Select a valid option");

            #endregion

            #region Saving_Account_Id_From_Pay

            // Conditions
            bool savingAccountIdFromPayIsValidOption = ccpsvm.SavingAccountIdFromPay != 0;
            bool savingAccountFromPayHasEnoughMoney = savingAccountFromPay != null && savingAccountFromPay.Balance >= ccpsvm.Amount;

            if (!savingAccountIdFromPayIsValidOption) errors.Add("InvalidSavingAccountIdOption", "Select a valid option");
            else if (amountIsValid && !savingAccountFromPayHasEnoughMoney) errors.Add("InvalidBalance", "The account to be debited does not have sufficient balance");

            #endregion

            return errors;
        }
    }
}
