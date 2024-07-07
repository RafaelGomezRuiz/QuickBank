using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class FacilityService : IFacilityService
    {
        // Repos
        private readonly ICreditCardRepository creditCardRepository;
        private readonly ISavingAccountRepository savingAccountRepository;

        // Services
        private readonly ILogService logService;

        // Tools
        private readonly IMapper mapper;

        public FacilityService(
            ICreditCardRepository creditCardRepository,
            ISavingAccountRepository savingAccountRepository,
            ILogService logService,
            IMapper mapper
        )
        {
            this.creditCardRepository = creditCardRepository;
            this.savingAccountRepository = savingAccountRepository;
            this.logService = logService;
            this.mapper = mapper;
        }

    }
}
