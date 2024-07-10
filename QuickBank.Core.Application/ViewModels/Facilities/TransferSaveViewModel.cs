using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Facilities
{
    public class TransferSaveViewModel
    {
        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int SavingAccountOriginId { get; set; }

        [Required(ErrorMessage = "Select a valid option")]
        [Range(1, int.MaxValue)]
        public int SavingAccountDestinyId { get; set; }

        [Required(ErrorMessage = "The amount field cannot be empty")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
