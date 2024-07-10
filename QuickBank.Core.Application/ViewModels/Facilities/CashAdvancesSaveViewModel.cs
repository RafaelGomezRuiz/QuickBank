using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Application.ViewModels.Products.SavingAccount;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Facilities
{
    public class CashAdvancesSaveViewModel
    {
        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int SavingAccountId { get; set; }

        [Required(ErrorMessage = "The amount field cannot be empty")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
