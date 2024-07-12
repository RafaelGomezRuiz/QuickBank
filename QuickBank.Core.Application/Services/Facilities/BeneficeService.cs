using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Domain.Entities.Facilities;

namespace QuickBank.Core.Application.Services.Facilities
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

        public async Task<List<BeneficeViewModel>?> GetAllWithIncludeAsync(List<string> includes)
        {
            var benefices = await beneficeRepository.GetAllWithIncludeAsync(includes);
            return mapper.Map<List<BeneficeViewModel>>(benefices);
        }

        public async Task<List<BeneficeViewModel>?> GetAllByUserIdAsync(string userId)
        {
            var beneficesWithIncludes = await GetAllWithIncludeAsync(new() { "BenefitedSavingAccount" });
            return beneficesWithIncludes?.Where(bvm => bvm.OwnerId == userId).ToList();
        }
    }
}
