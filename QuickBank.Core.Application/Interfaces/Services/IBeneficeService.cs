using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Domain.Entities;

namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface IBeneficeService : IGenericService<BeneficeSaveViewModel, BeneficeViewModel, BeneficeEntity>
    {
    }
}
