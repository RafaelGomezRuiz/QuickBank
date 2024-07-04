namespace QuickBank.Core.Application.ViewModels.Products
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public double Balance { get; set; }
        public double LimitCredit { get; set; }
        public int Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? UserId { get; set; }
    }
}
