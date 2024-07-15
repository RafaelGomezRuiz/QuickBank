using AutoMapper;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Domain.Entities.Facilities;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class BeneficeService : GenericService<BeneficeSaveViewModel, BeneficeViewModel, BeneficeEntity>, IBeneficeService
    {
        protected readonly IBeneficeRepository beneficeRepository;
        protected readonly ISavingAccountService savingAccountService;
        protected readonly IUserService userService;
        protected readonly IUserHelper userHelper;
        protected readonly IMapper mapper;

        public BeneficeService(
            IBeneficeRepository beneficeRepository,
            ISavingAccountService savingAccountService,
            IUserService userService,
            IUserHelper userHelper,
            IMapper mapper
        ) 
        : base(beneficeRepository, mapper)
        {
            this.beneficeRepository = beneficeRepository;
            this.savingAccountService = savingAccountService;
            this.userService = userService;
            this.userHelper = userHelper;
            this.mapper = mapper;
        }

        public async Task<List<BeneficeViewModel>?> GetAllWithIncludeAsync(List<string> includes)
        {
            var benefices = await beneficeRepository.GetAllWithIncludeAsync(includes);
            return mapper.Map<List<BeneficeViewModel>>(benefices);
        }

        public async Task<List<BeneficeViewModel>?> GetAllByUserIdAsync(string userId)
        {
            return (await GetAllAsync()).Where(bvm => bvm.OwnerId == userId).ToList();
        }

        public async Task<List<BeneficeViewModel>?> GetAllByUserIdWithIncludeAsync(string userId, List<string> includes)
        {
            return (await GetAllWithIncludeAsync(includes)).Where(bvm => bvm.OwnerId == userId).ToList();
        }

        public async Task<List<BeneficeViewModel>> GetAllWithFullNameAsync()
        {
            var benefices = await GetAllWithIncludeAsync(new() { "BenefitedSavingAccount" });
            var users = await userService.GetAllAsync();

            foreach (var benefice in benefices)
            {
                var user = users.FirstOrDefault(user => user.Id == benefice.BenefitedSavingAccount.OwnerId);
                benefice.BenefitedFullName = $"{user.UserName} {user.LastName}";
            }

            return benefices;
        }

        public async Task<List<BeneficeViewModel>> GetAllByUserIdWithFullNameAsync(string userId)
        {
            return (await GetAllWithFullNameAsync()).Where(bvm => bvm.OwnerId == userId).ToList();
        }

        public async Task<BeneficeViewModel?> GetByIdWithFullNameAsync(int beneficeId)
        {
            return (await GetAllWithFullNameAsync()).FirstOrDefault(bvm => bvm.Id == beneficeId);
        }

        public new async Task AddAsync(BeneficeSaveViewModel bsvm)
        {
            // Create the entity
            var entity = new BeneficeEntity
            {
                OwnerId = userHelper.GetUser()!.Id,
                BenefitedSavingAccountId = (await savingAccountService.GetViewModelByNumberAccountAsync(bsvm.NumberAccount))!.Id
            };

            await beneficeRepository.AddAsync(entity);
        }
    }
}
