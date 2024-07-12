using AutoMapper;
using QuickBank.Core.Application.Enums;
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
        public async Task<List<LoanViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await base.GetAllAsync()).Where(savm => savm.UserId == userId).ToList();
        }
    }
}
