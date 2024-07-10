using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class CreditCardPaySaveViewModel
    {
        public int CreditCardIdToPay { get; set; }

        public int SavingAccountIdFromPay { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
