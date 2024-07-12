using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Application.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Payments
{
    public class BeneficiaryPaySaveViewModel
    {
        public int BeneficeIdToPay { get; set; }

        public int SavingAccountIdFromPay { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public List<BeneficeViewModel>? Benefits { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
    }
}
