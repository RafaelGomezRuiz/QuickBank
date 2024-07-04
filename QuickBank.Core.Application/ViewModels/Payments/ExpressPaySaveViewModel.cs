using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class ExpressPaySaveViewModel
    {
        [Required(ErrorMessage = "The number account field cannot be empty")]
        [DataType(DataType.Text)]
        public string NumberAccountToPay { get; set; }

        [Required(ErrorMessage = "The amount field cannot be empty")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int AccountIdFromPay { get; set; }

        public List<SavingAccountViewModel>? Accounts { get; set; }
    }
}
