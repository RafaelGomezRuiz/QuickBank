namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface IProductValidationService
    {
        Task<Dictionary<string, string>> AvailableSavingAccounts();
        Task<Dictionary<string, string>> AvailableLoans();
        Task<Dictionary<string, string>> AvailableCreditCards();
    }
}
