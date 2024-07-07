using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface ISavingAccountService : IGenericService<SavingAccountViewModel, SavingAccountViewModel, SavingAccountEntity>
    {
        Task<List<SavingAccountViewModel>?> GetAllByUserIdAsync(string userId);
        Task<SavingAccountViewModel?> GetViewModelByNumberAccountAsync(string numberAccount);
    }
}
