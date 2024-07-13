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
    public class LoanService : GenericService<LoanViewModel, LoanViewModel, LoanEntity>, ILoanService
    {
        protected readonly ILoanRepository loanRepository;
        protected readonly IMapper mapper;

        public LoanService(ILoanRepository loanRepository, IMapper mapper) : base(loanRepository, mapper)
        {
            this.loanRepository = loanRepository;
            this.mapper = mapper;
        }
        public async Task<LoanViewModel> GetAvailableLoanAsync()
        {
            return (await base.GetAllAsync()).FirstOrDefault(loan => loan.Status == (int)EProductStatus.INACTIVE && loan.UserId == null);
        }
        public async Task<List<LoanViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(loan => loan.UserId == userId).ToList();
        }

        public async Task<List<LoanViewModel>?> GetActiveAsync()
        {
            return (await base.GetAllAsync()).Where(savm => savm.Status == (int)EProductStatus.ACTIVE).ToList();
        }
        public async Task<List<LoanViewModel>?> GetAllByUserIdWithBalanceAsync(string userId)
        {
            var loans = await GetAllByUserIdAsync(userId);
            return loans?.Where(loan => loan.Amount > 0).ToList();
        }
        public async Task SetLoan(LoanSaveViewModel setLoan)
        {
            string newLoanNumber = CodeStringGenerator.GenerateProductNumber();
            bool loanNumberExists = (await base.GetAllAsync()).Any(loan => loan.LoanNumber == newLoanNumber);

            var loanToSet = await GetAvailableLoanAsync();

            if (loanToSet == null)
            {
                throw new InvalidOperationException("No available saving accounts.");
            }

            while (loanNumberExists)
            {
                newLoanNumber = CodeStringGenerator.GenerateProductNumber();
            }

            loanToSet.Status = (int)EProductStatus.ACTIVE;
            loanToSet.Amount = setLoan.Amount;
            loanToSet.UserId = setLoan.OwnerId;
            loanToSet.LoanNumber = newLoanNumber;
            var loanEntity = mapper.Map<LoanEntity>(loanToSet);
            await loanRepository.UpdateAsync(loanEntity, loanEntity.Id);
        }
    }
}
