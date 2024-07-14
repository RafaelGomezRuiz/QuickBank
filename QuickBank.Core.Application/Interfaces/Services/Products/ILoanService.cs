using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface ILoanService : IGenericService<LoanViewModel, LoanViewModel, LoanEntity>
    {
        Task<List<LoanViewModel>?> GetAllByUserIdAsync(string userId);
        Task<List<LoanViewModel>?> GetAllByUserIdWithBalanceAsync(string userId);
        Task SetLoan(LoanSaveViewModel setLoan);
        Task<List<LoanViewModel>?> GetActiveAsync();
        Task<LoanViewModel> GetAvailableLoanAsync();

    }
}
