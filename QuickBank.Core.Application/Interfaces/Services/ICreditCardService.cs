using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface ICreditCardService: IGenericService<CreditCardPaySaveViewModel, CreditCardViewModel, CreditCardEntity>
    {
    }
}
