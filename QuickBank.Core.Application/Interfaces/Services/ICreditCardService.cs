﻿using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface ICreditCardService: IGenericService<CreditCardViewModel, CreditCardViewModel, CreditCardEntity>
    {
    }
}
