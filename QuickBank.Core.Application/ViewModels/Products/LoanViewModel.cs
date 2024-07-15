namespace QuickBank.Core.Application.ViewModels.Products
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public string LoanNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Deadline { get; set; }
        public double InterestRate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime AprovalDate { get; set; }
        public string? OwnerId { get; set; }
    }
}
