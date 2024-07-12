using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.ViewModels.User
{
    public class UserProductsViewModel
    {
        public string OwnerId { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<LoanViewModel>? Loans { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }
    }
}
