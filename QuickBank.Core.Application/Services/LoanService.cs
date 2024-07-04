using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Services
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
    }
}
