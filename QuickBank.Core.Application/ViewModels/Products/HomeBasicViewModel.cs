using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Core.Application.ViewModels.Products
{
    public class HomeBasicViewModel
    {
        public List<SavingAccountViewModel> SavingAccounts { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<LoanViewModel>? Loans { get; set; }

    }
}
