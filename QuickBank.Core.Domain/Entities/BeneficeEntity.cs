using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Domain.Entities
{
    public class BeneficeEntity
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int BenefitedSavingAccountId { get; set; }
        public SavingAccountEntity? BenefitedSavingAccount { get; set; }
    }
}
