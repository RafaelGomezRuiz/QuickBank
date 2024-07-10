﻿using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Services.Products
{
    public class CreditCardService : GenericService<CreditCardViewModel, CreditCardViewModel, CreditCardEntity>, ICreditCardService
    {
        protected readonly ICreditCardRepository creditCardRepository;
        protected readonly IMapper mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper) : base(creditCardRepository, mapper)
        {
            this.creditCardRepository = creditCardRepository;
            this.mapper = mapper;
        }

        public async Task<List<CreditCardViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(ccvm => ccvm.UserId == userId).ToList();
        }

        public async Task<List<CreditCardViewModel>?> GetAllByUserIdWithBalanceAsync(string userId)
        {
            var creditCards = await GetAllByUserIdAsync(userId);
            return creditCards?.Where(ccvm => ccvm.Balance > 0).ToList();
        }
    }
}
