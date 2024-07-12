using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class LoanPaySaveViewModel
    {
        public int LoanIdToPay { get; set; }

        public int SavingAccountIdFromPay { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<LoanViewModel>? Loans { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
