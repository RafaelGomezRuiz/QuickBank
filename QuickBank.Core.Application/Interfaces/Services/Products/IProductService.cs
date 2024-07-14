namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface IProductService
    {
        Task<int> GetAssignedProducts();
    }
}
