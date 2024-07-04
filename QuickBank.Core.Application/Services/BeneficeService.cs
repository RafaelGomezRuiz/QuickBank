using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Domain.Entities;

namespace QuickBank.Core.Application.Services
{
    public class BeneficeService : GenericService<BeneficeSaveViewModel, BeneficeViewModel, BeneficeEntity>, IBeneficeService
    {
        protected readonly IBeneficeRepository beneficeRepository;
        protected readonly IMapper mapper;

        public BeneficeService(IBeneficeRepository beneficeRepository, IMapper mapper) : base(beneficeRepository, mapper) 
        {
            this.beneficeRepository = beneficeRepository;
            this.mapper = mapper;
        }
    }
}
