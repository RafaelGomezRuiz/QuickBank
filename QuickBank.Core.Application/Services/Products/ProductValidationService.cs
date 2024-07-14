using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Services.Products
{
    public class ProductValidationService : IProductValidationService
    {
        protected readonly ISavingAccountService savingAccountService;
        protected readonly ILoanService loanService;
        protected readonly ICreditCardService creditCardService;

        public ProductValidationService(
            ISavingAccountService savingAccountService,
            ILoanService loanService,
            ICreditCardService creditCardService)
        {
            this.savingAccountService = savingAccountService;
            this.loanService = loanService;
            this.creditCardService = creditCardService;
        }
        public async Task<Dictionary<string, string>> AvailableSavingAccounts()
        {
            var errors = new Dictionary<string, string>();
            var availableSavingAcounts = await savingAccountService.GetAvailableSavingAccountAsync();
            if (availableSavingAcounts == null )
                errors.Add("ArentAvailableSavingsAccounts", "The aren't saving accounts");
            return errors;
        }
        public async Task<Dictionary<string, string>> AvailableLoans()
        {
            var errors = new Dictionary<string, string>();
            var availableLoans = await loanService.GetAvailableLoanAsync();
            if (availableLoans == null)
                errors.Add("ArentAvailableLoans", "The aren't available loans");
            return errors;
        }
        public async Task<Dictionary<string, string>> AvailableCreditCards()
        {
            var errors = new Dictionary<string, string>();
            var availableCreditCards = await creditCardService.GetAvailableCreditCardsAsync();
            if (availableCreditCards == null)
                errors.Add("ArentAvailableCreditCards", "The aren't available credit cards");
            return errors;
        }

    }
}
