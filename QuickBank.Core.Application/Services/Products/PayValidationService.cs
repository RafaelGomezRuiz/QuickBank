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

            // Conditional vars
            bool amountIsValid = epsvm.Amount >= BusinessLogicConstantsHelper.MinimumPaymentAmount;
            bool numberAccountCharactersIsValid = epsvm.NumberAccountToPay != null && epsvm.NumberAccountToPay.Length == BusinessLogicConstantsHelper.MaxLengthNumberAccount;
            bool savingAccountToPayExists = numberAccountCharactersIsValid && savingAccountToPay != null;
            bool savingAccountToPayIsValid = savingAccountToPayExists && savingAccountToPay.Status == (int)EProductStatus.ACTIVE;
            bool savingAccountFromPayHasEnoughMoney = savingAccountFromPay != null && savingAccountFromPay.Balance >= epsvm.Amount;


            // Insuficient money to do the payment
            if (!amountIsValid)
            {
                errors.Add("InvalidAmount", $"You must enter a valid amount, {BusinessLogicConstantsHelper.MinimumPaymentAmount} minimun");
            }
            
            if (!numberAccountCharactersIsValid) // Invalid number of characters in account number
            {
                errors.Add("InvalidNumberCharacters", $"The number of characters is {BusinessLogicConstantsHelper.MaxLengthNumberAccount} minimun");
            }
            else if (!savingAccountToPayExists) // Invalid number account to pay
            {
                errors.Add("InvalidNumberAccount", $"The account number is not valid");
            }
            else if (!savingAccountToPayIsValid) // Check if Saving account to pay is active
            {
                errors.Add("InvalidSavingAccount", $"This account is not available to deposit");
            }

            // Check if saving account from pay has the enought money to pay
            if (!savingAccountFromPayHasEnoughMoney)
            {
                errors.Add("InvalidBalance", $"The account to be debited does not have sufficient balance");
            }

            return errors;
        }
    }
}
