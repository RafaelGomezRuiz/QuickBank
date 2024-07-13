using AutoMapper;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.Commons;
using QuickBank.Core.Application.Services.Products;
using QuickBank.Core.Application.Services.User;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Application.ViewModels.Products;
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
            IMapper mapper) : base(beneficeRepository, mapper)
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
            var beneficesWithIncludes = await GetAllWithIncludeAsync(new() { "BenefitedSavingAccount" });
            return beneficesWithIncludes?.Where(bvm => bvm.OwnerId == userId).ToList();
        }

        //
        public async Task<List<BeneficeViewModel>> GetBeneficiariesWithFullNameAsync()
        {
            var user = userHelper.GetUser();
            var beneficiaries = await beneficeRepository.GetAllAsync();
            var userBeneficiaries = beneficiaries.Where(b => b.OwnerId == user.Id).ToList();
            var beneficeViewModels = new List<BeneficeViewModel>();

            foreach (var beneficiary in userBeneficiaries)
            {
                var beneficeViewModel = await MapToViewModelAsync(beneficiary);
                beneficeViewModels.Add(beneficeViewModel);
            }

            return beneficeViewModels;
        }

        public async Task<BeneficeViewModel?> GetBeneficiaryByIdAsync(int id)
        {
            var beneficiary = await beneficeRepository.GetByIdAsync(id);
            if (beneficiary == null)
            {
                return null;
            }
            return await MapToViewModelAsync(beneficiary);
        }

        public override async Task<BeneficeSaveViewModel?> AddAsync(BeneficeSaveViewModel model)
        {
            var savingAccount = await savingAccountService.GetViewModelByNumberAccountAsync(model.NumberAccount);
            var user = userHelper.GetUser();

            if (savingAccount != null)
            {
                var entity = new BeneficeEntity
                {
                    OwnerId = user.Id,
                    BenefitedSavingAccountId = savingAccount.Id
                };
                var addedEntity = await beneficeRepository.AddAsync(entity);
                return mapper.Map<BeneficeSaveViewModel>(addedEntity);
            }
            else
            {
                throw new Exception("The account number does not exist.");
            }
        }

        public async Task<BeneficeViewModel> MapToViewModelAsync(BeneficeEntity beneficiary)
        {
            var beneficeViewModel = mapper.Map<BeneficeViewModel>(beneficiary);
            var savingAccount = await savingAccountService.GetByIdAsync(beneficiary.BenefitedSavingAccountId);
            var accountOwner = await userService.FindyByIdAsync(savingAccount.UserId);

            beneficeViewModel.BenefitedSavingAccount = mapper.Map<SavingAccountViewModel>(savingAccount);
            beneficeViewModel.BenefitedFullName = $"{accountOwner.FirstName} {accountOwner.LastName}";

            return beneficeViewModel;
        }
    }
}
