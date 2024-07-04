using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Services
{
    public class CreditCardService : GenericService<CreditCardPaySaveViewModel,CreditCardViewModel,CreditCardEntity>,ICreditCardService
    {
        protected readonly ICreditCardRepository _creditCardRepository;
        protected readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository _creditCardRepository, IMapper _mapper) : base(_creditCardRepository, _mapper)
        {
            this._creditCardRepository = _creditCardRepository;
            this._mapper = _mapper;
        }
    }
}
