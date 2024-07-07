using QuickBank.Core.Application.Interfaces.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface ICreditCardService : IGenericService<CreditCardViewModel, CreditCardViewModel, CreditCardEntity>
    {
    }
}
