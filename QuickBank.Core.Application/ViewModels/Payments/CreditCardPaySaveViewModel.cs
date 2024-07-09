using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Application.ViewModels.Products.SavingAccount;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class CreditCardPaySaveViewModel
    {
        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int CreditCardIdToPay { get; set; }

        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int SavingAccountIdFromPay { get; set; }

        [Required(ErrorMessage = "The amount field cannot be empty")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
