using AutoMapper;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products.SavingAccount;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Services.Products
{
    public class SavingAccountService : GenericService<SavingAccountViewModel, SavingAccountViewModel, SavingAccountEntity>, ISavingAccountService
    {
        protected readonly ISavingAccountRepository savingAccountRepository;
        protected readonly IMapper mapper;

        public SavingAccountService(ISavingAccountRepository savingAccountRepository, IMapper mapper) : base(savingAccountRepository, mapper)
        {
            this.savingAccountRepository = savingAccountRepository;
            this.mapper = mapper;
        }

        public async Task<SavingAccountViewModel> GetAvailableSavingAccountAsync()
        {
            return (await base.GetAllAsync()).FirstOrDefault(savm => savm.Status == 0 && savm.UserId == null);
        }

        public async Task<List<SavingAccountViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(savm => savm.UserId == userId).ToList();
        }

        public async Task<SavingAccountViewModel?> GetViewModelByNumberAccountAsync(string numberAccount)
        {
            return (await base.GetAllAsync()).FirstOrDefault(savm => savm.AccountNumber == numberAccount);
        }

        public async Task SetSavingAccount(SetSavingAccount setSavingAccount)
        {
            var userAccounts=await GetAllByUserIdAsync(setSavingAccount.UserId);
            bool isFirstAccount = userAccounts.Count == 0;
            string accountNumber = CodeStringGenerator.GenerateProductNumber(); ;
            bool accountNumberExists = (await base.GetAllAsync()).Any(savm => savm.AccountNumber == accountNumber);

            var savingAccountVm = await GetAvailableSavingAccountAsync();

            if (savingAccountVm == null)
            {
                throw new InvalidOperationException("No available saving accounts.");
            }

            do
            {
                accountNumber = CodeStringGenerator.GenerateProductNumber();
            } while (accountNumberExists);

            savingAccountVm.Status = 1;
            savingAccountVm.Principal = isFirstAccount;
            savingAccountVm.Balance = setSavingAccount.InitialAmount;
            savingAccountVm.UserId = setSavingAccount.UserId;
            savingAccountVm.AccountNumber = accountNumber;
            var savingAccountEntity = mapper.Map<SavingAccountEntity>(savingAccountVm);
            await savingAccountRepository.UpdateAsync(savingAccountEntity, savingAccountEntity.Id);
        }
            
    }
}
