using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class FacilityValidationService : IFacilityValidationService
    {
        private readonly ISavingAccountService _savingAccountService;

        public FacilityValidationService(ISavingAccountService savingAccountService)
        {
            _savingAccountService = savingAccountService;
        }

        public async Task<Dictionary<string, string>> ValidateTransfer(TransferSaveViewModel model)
        {
            var errors = new Dictionary<string, string>();

            var originAccount = await _savingAccountService.GetByIdAsync(model.SavingAccountOriginId);
            var destinyAccount = await _savingAccountService.GetByIdAsync(model.SavingAccountDestinyId);

            #region Account_Validation

            bool sameAccount = model.SavingAccountOriginId == model.SavingAccountDestinyId;
            bool originAccountExists = originAccount != null;
            bool destinyAccountExists = destinyAccount != null;

            if (model.SavingAccountOriginId == 0) errors.Add("InvalidOriginAccountSelection", "Please select a valid origin account.");
            else if (!originAccountExists) errors.Add("InvalidOriginAccount", "The origin account is not valid.");
            else if(model.SavingAccountDestinyId == 0) errors.Add("InvalidDestinyAccountSelection", "Please select a valid destination account.");
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
    }
}