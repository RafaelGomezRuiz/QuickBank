using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class ExpressPaySaveViewModel
    {
        [DataType(DataType.Text)]
        public string NumberAccountToPay { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public int AccountIdFromPay { get; set; }

        public List<SavingAccountViewModel>? Accounts { get; set; }
    }
}
