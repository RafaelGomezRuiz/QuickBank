using AutoMapper;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
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

        public async Task<CreditCardViewModel> GetAvailableCreditCardsAsync()
        {
            return (await base.GetAllAsync()).FirstOrDefault(loan => loan.Status == (int)EProductStatus.INACTIVE && loan.OwnerId == null);
        }
        public async Task<List<CreditCardViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(ccvm => ccvm.OwnerId == userId).ToList();
        }
        public async Task<List<CreditCardViewModel>?> GetActiveAsync()
        {
            return (await base.GetAllAsync()).Where(savm => savm.Status == (int)EProductStatus.ACTIVE).ToList();
        }
        public async Task<List<CreditCardViewModel>?> GetAllByUserIdWithBalanceAsync(string userId)
        {
            var creditCards = await GetAllByUserIdAsync(userId);
            return creditCards?.Where(ccvm => ccvm.Balance > 0).ToList();
        }
        public async Task SetCreditCard(CreditCardSaveViewModel setCreditCard)
        {
            string newCreditCardNumber = CodeStringGenerator.GenerateProductNumber();
            bool creditCardNumberExists = (await base.GetAllAsync()).Any(loan => loan.CardNumber == newCreditCardNumber);

            var creditCardToSet = await GetAvailableCreditCardsAsync();

            if (creditCardToSet == null)
            {
                throw new InvalidOperationException("No available creditCardToSet.");
            }

            while (creditCardNumberExists)
            {
                newCreditCardNumber = CodeStringGenerator.GenerateProductNumber();
            }

            creditCardToSet.Status = (int)EProductStatus.ACTIVE;
            creditCardToSet.LimitCredit = setCreditCard.AmountAvailable;
            creditCardToSet.OwnerId = setCreditCard.OwnerId;
            creditCardToSet.CardNumber = newCreditCardNumber;
            var creditCardEntity = mapper.Map<CreditCardEntity>(creditCardToSet);
            await creditCardRepository.UpdateAsync(creditCardEntity, creditCardEntity.Id);
        }
    }
}
