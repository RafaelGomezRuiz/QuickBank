using QuickBank.Core.Application.Interfaces.Services.Products;

namespace QuickBank.Core.Application.Services.Products
{
    public class ProductService : IProductService
    {
        protected readonly ISavingAccountService savingAccountService;
        protected readonly ILoanService loanService;
        protected readonly ICreditCardService creditCardService;

        public ProductService(
            ISavingAccountService savingAccountService,
            ILoanService loanService,
            ICreditCardService creditCardService
            )
        {
            this.savingAccountService = savingAccountService;
            this.loanService = loanService;
            this.creditCardService = creditCardService;
        }
        public async Task<int> GetAssignedProducts()
        {
            var savingAccounts = (await savingAccountService.GetAllActiveAsync()).Count;
            var loans = (await loanService.GetActiveAsync()).Count;
            var creditCards = (await creditCardService.GetActiveAsync()).Count;
            return savingAccounts + loans + creditCards;
        }


    }
}
