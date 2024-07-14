using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface ICreditCardService : IGenericService<CreditCardViewModel, CreditCardViewModel, CreditCardEntity>
    {
        Task<List<CreditCardViewModel>?> GetAllByUserIdAsync(string userId);
        Task<List<CreditCardViewModel>?> GetAllByUserIdWithBalanceAsync(string userId);
        Task SetCreditCard(CreditCardSaveViewModel setCreditCard);
        Task<List<CreditCardViewModel>?> GetActiveAsync();
        Task<CreditCardViewModel> GetAvailableCreditCardsAsync();
    }
}
