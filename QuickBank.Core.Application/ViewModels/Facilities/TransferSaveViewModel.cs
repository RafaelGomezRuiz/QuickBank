using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Facilities
{
    public class TransferSaveViewModel
    {
        public int SavingAccountOriginId { get; set; }
        public int SavingAccountDestinyId { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
