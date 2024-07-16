using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface ISavingAccountService : IGenericService<SavingAccountViewModel, SavingAccountViewModel, SavingAccountEntity>
    {
        Task<SavingAccountViewModel?> GetAvailableSavingAccountAsync();
        Task<SavingAccountViewModel?> GetPrincipalSavingAccountAsync(string userId);
        Task<List<SavingAccountViewModel>?> GetAllActiveAsync();
        Task<SavingAccountViewModel?> GetViewModelByNumberAccountAsync(string numberAccount);
        Task SetSavingAccount(SetSavingAccount setSavingAccount);
        Task<List<SavingAccountViewModel>?> GetAllByUserIdAsync(string userId);
    }   
}
