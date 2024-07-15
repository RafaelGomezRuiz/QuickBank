using QuickBank.Core.Application.ViewModels.Facilities.Benefice;

namespace QuickBank.Core.Application.ViewModels.Products
{
    public class SavingAccountViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public bool Principal { get; set; }
        public string? OwnerId { get; set; }

        // Navegation properties
        public ICollection<BeneficeViewModel>? Benefices { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
