using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Services
{
    public class CreditCardService : GenericService<CreditCardViewModel, CreditCardViewModel, CreditCardEntity>,ICreditCardService
    {
        protected readonly ICreditCardRepository creditCardRepository;
        protected readonly IMapper mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper) : base(creditCardRepository, mapper)
        {
            this.creditCardRepository = creditCardRepository;
            this.mapper = mapper;
        }
    }
}
