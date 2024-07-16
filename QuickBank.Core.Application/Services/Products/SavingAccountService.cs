using AutoMapper;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.ViewModels.Products;
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

        public async Task<SavingAccountViewModel?> GetAvailableSavingAccountAsync()
        {
            return (await base.GetAllAsync()).FirstOrDefault(savm => savm.Status == (int)EProductStatus.INACTIVE && savm.OwnerId == null);
        }

        public async Task<SavingAccountViewModel?> GetPrincipalSavingAccountAsync(string userId)
        {
            return (await base.GetAllAsync()).FirstOrDefault(savm => savm.Principal == true && savm.OwnerId == userId);
        }

        public async Task<List<SavingAccountViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(savm => savm.OwnerId == userId).ToList();
        }

        public async Task<List<SavingAccountViewModel>?> GetAllActiveAsync()
        {
            return (await base.GetAllAsync()).Where(savm => savm.Status == (int)EProductStatus.ACTIVE).ToList();
        }

        public async Task<SavingAccountViewModel?> GetViewModelByNumberAccountAsync(string numberAccount)
        {
            return (await base.GetAllAsync()).FirstOrDefault(savm => savm.AccountNumber == numberAccount);
        }

        public async Task SetSavingAccount(SetSavingAccount setSavingAccount)
        {
            // Get the available saving account
            var savingsAccountAvailable = await GetAvailableSavingAccountAsync();

            // Set the random account number
            while (true)
            {
                string productRandomNumber = CodeStringGenerator.GenerateProductNumber();
                bool productNumberExists = (await base.GetAllAsync()).Any(savm => savm.AccountNumber == productRandomNumber);

                if (!productNumberExists)
                {
                    savingsAccountAvailable.AccountNumber = productRandomNumber;
                    break;
                }
            }

            // Set the principal of secondary account
            savingsAccountAvailable.Principal = GetPrincipalSavingAccountAsync(setSavingAccount.UserId) != null;

            // Set some default values of saving account
            savingsAccountAvailable.Status = (int)EProductStatus.ACTIVE;
            savingsAccountAvailable.Balance = setSavingAccount.InitialAmount;
            savingsAccountAvailable.OwnerId = setSavingAccount.UserId;

            // Update the entity
            await base.UpdateAsync(savingsAccountAvailable, savingsAccountAvailable.Id);
        }


        public override async Task DeleteAsync(int entityId)
        {
            // Get saving account to delete
            var savingAccountToDelete = await GetByIdAsync(entityId);

            // Check if the saving account have balance
            if (savingAccountToDelete.Balance > 0)
            {
                var principalSavingAccount = await GetPrincipalSavingAccountAsync(savingAccountToDelete.OwnerId);
                principalSavingAccount.Balance += savingAccountToDelete.Balance;
                await UpdateAsync(principalSavingAccount, principalSavingAccount.Id);
            }

            // Delete the saving account
            await base.DeleteAsync(entityId);
        }
    }
}
