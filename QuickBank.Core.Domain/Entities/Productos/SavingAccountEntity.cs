namespace QuickBank.Core.Domain.Entities.Productos
{
    public class SavingAccountEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public bool Principal { get; set; }
        public string? UserId { get; set; }
    }
}
