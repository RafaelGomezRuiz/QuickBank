using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;

namespace QuickBank.Core.Application.Services
{
    public class PayService : IPayService
    {
        // Repos
        private readonly IBeneficeRepository beneficeRepository;
        private readonly ICreditCardRepository creditCardRepository;
        private readonly ILoanRepository loanRepository;
        private readonly ISavingAccountRepository savingAccountRepository;

        // Services
        private readonly ILogService logService;

        // Tools
        private readonly IMapper mapper;

        public PayService(
            IBeneficeRepository beneficeRepository,
            ICreditCardRepository creditCardRepository,
            ILoanRepository loanRepository,
            ISavingAccountRepository savingAccountRepository,
            ILogService logService,
            IMapper mapper
        )
        {
            this.beneficeRepository = beneficeRepository;
            this.creditCardRepository = creditCardRepository;
            this.loanRepository = loanRepository;
            this.savingAccountRepository = savingAccountRepository;
            this.logService = logService;
            this.mapper = mapper;
        }


    }
}
