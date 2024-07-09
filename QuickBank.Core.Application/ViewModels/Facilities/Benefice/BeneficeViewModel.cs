using QuickBank.Core.Application.ViewModels.Products.SavingAccount;

namespace QuickBank.Core.Application.ViewModels.Facilities.Benefice
{
    public class BeneficeViewModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int BenefitedSavingAccountId { get; set; }
        public SavingAccountViewModel? BenefitedSavingAccount { get; set; }
        public string BenefitedFullName { get; set; }
    }
}
