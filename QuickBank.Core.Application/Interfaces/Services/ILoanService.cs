using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<LoanViewModel, LoanViewModel, LoanEntity>
    {
    }
}
