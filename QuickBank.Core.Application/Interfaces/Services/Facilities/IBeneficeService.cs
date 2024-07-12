using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Domain.Entities.Facilities;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IBeneficeService : IGenericService<BeneficeSaveViewModel, BeneficeViewModel, BeneficeEntity>
    {
        Task<List<BeneficeViewModel>?> GetAllWithIncludeAsync(List<string> includes);
        Task<List<BeneficeViewModel>?> GetAllByUserIdAsync(string userId);
    }
}
