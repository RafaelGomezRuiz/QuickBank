using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.Facilities;
using QuickBank.Core.Application.Services.User;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Application.ViewModels.User;
using QuickBank.Helpers;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdministrationUserController : Controller
    {
        protected readonly IUserService userService;
        protected readonly ISavingAccountService savingAccountService;
        protected readonly ILoanService loanService;
        protected readonly ICreditCardService creditCardService;
        protected readonly IUserValidationService userValidationService;
        protected readonly IProductValidationService productValidationService;


        public AdministrationUserController(
            IUserService userService,
            ISavingAccountService savingAccountService,
            ILoanService loanService,
            ICreditCardService creditCardService,
            IUserValidationService userValidationService,
            IProductValidationService productValidationService)
        {
            this.userService = userService;
            this.savingAccountService = savingAccountService;
            this.loanService = loanService;
            this.creditCardService = creditCardService;
            this.userValidationService= userValidationService;
            this.productValidationService = productValidationService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetAllAsync());
        }
        public async Task<IActionResult> Add()
        {
            return View("SaveUser", new UserSaveViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserSaveViewModel userSaveViewModel)
        {
            ModelState.AddModelErrorRange(await userValidationService.UserSaveValidation(userSaveViewModel));
            ModelState.AddModelErrorRange(await userValidationService.PasswordValidation(userSaveViewModel));
            ModelState.AddModelErrorRange(await productValidationService.AvailableSavingAccounts());

            if (!ModelState.IsValid)
            {
                return View("SaveUser", userSaveViewModel);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await userService.RegisterAsync(userSaveViewModel, origin);
            if (response.HasError)
            {
                userSaveViewModel.HasError = response.HasError;
                userSaveViewModel.ErrorDescription = response.ErrorDescription;
                return View("SaveUser", userSaveViewModel);
            }
            if (userSaveViewModel.UserType == ERoles.BASIC)
            {
                SetSavingAccount setSavingAccount = new()
                {
                    UserId = response.Id,
                    InitialAmount = (double)userSaveViewModel.InitialAmount
                };
            }
            return RedirectRoutesHelper.routeAdminHome;
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserState(string id)
        {
            UserSaveViewModel user = await userService.FindyByIdAsync(id);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserStatePost(string id)
        {
            UserSaveViewModel user = await userService.FindyByIdAsync(id);

            user.Status = (user.Status != (int)EUserStatus.ACTIVE) ? (int)EUserStatus.ACTIVE : (int)EUserStatus.INACTIVE;
            await userService.UpdateUserAsync(user);

            return RedirectRoutesHelper.routeAdmininistrationUserIndex;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserSaveViewModel user = await userService.FindyByIdAsync(id);
            return View("SaveUser", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserSaveViewModel userSaveViewModel)
        {
            ModelState.AddModelErrorRange(await userValidationService.UserSaveValidation(userSaveViewModel));
            ModelState.AddModelErrorRange(await userValidationService.PasswordValidation(userSaveViewModel));
            ModelState.AddModelErrorRange(await productValidationService.AvailableSavingAccounts());

            if (!ModelState.IsValid)
            {
                return View("SaveUser", userSaveViewModel);
            }
            var updateResult = await userService.UpdateUserAsync(userSaveViewModel);

            if (userSaveViewModel.UserType == ERoles.BASIC)
            {
                SavingAccountViewModel savingAccountViewModel = await savingAccountService.GetPrincipalSavingAccountAsync(userSaveViewModel.Id);
                savingAccountViewModel.Balance += (double)userSaveViewModel.InitialAmount;
                savingAccountService.UpdateAsync(savingAccountViewModel, savingAccountViewModel.Id);
            }
            return RedirectRoutesHelper.routeAdmininistrationUserIndex;
        }

        [HttpGet]
        public async Task<IActionResult> UserProducts(string ownerId)
        {
            UserProductsViewModel userProducts = new()
            {
                OwnerId = ownerId,
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(ownerId),
                Loans = await loanService.GetAllByUserIdAsync(ownerId),
                CreditCards = await creditCardService.GetAllByUserIdAsync(ownerId)
            };
            return View(userProducts);
        }
        [HttpGet]
        public async Task<IActionResult> SetSavingAccount(string ownerId)
        {
            ModelState.AddModelErrorRange(await productValidationService.AvailableSavingAccounts());
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });
            }
            SetSavingAccount savingAccount = new(){ UserId = ownerId };

            await savingAccountService.SetSavingAccount(savingAccount);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });

        }
        [HttpGet]
        public async Task<IActionResult> SetLoan(string ownerId)
        {
            LoanSaveViewModel loanSaveViewModel = new() { OwnerId = ownerId};
            return View(loanSaveViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetLoan(LoanSaveViewModel loanSaveViewModel)
        {
            ModelState.AddModelErrorRange(await productValidationService.AvailableLoans());
            if (!ModelState.IsValid)
            {
                return View(loanSaveViewModel);
            }

            await loanService.SetLoan(loanSaveViewModel);
            return RedirectToRoute(new {Controller="AdministrationUser",Action="UserProducts",loanSaveViewModel.OwnerId});
        }

        [HttpGet]
        public async Task<IActionResult> SetCreditCard(string ownerId)
        {
            CreditCardSaveViewModel creditCardSaveViewModel = new(){OwnerId = ownerId,};
            return View(creditCardSaveViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCreditCard(CreditCardSaveViewModel ceditCardSaveViewModel)
        {
            await creditCardService.SetCreditCard(ceditCardSaveViewModel);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ceditCardSaveViewModel.OwnerId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSavingAccount(int id)
        {
            var savingAccount = await savingAccountService.GetByIdAsync(id);
            return View(savingAccount);
        }

        [HttpGet]
        //ojo con este nombre
        public async Task<IActionResult> DeleteSavingAccountPost(int id)
        {
            //Lazy alternativa es cambiaele a SavingAcountService y al vm de userId a Owner id haciendo una migracion
            SavingAccountViewModel savingAccountOwner= await savingAccountService.GetByIdAsync(id);

            await savingAccountService.DeleteAsync(id);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", savingAccountOwner.OwnerId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loanViewModel = await loanService.GetByIdAsync(id);
            return View(loanViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoanPost(int id)
        {
            LoanViewModel loanOwnerId = await loanService.GetByIdAsync(id);
            string ownerId =loanOwnerId.UserId;
            await loanService.DeleteAsync(id);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId});
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCreditCard(int id)
        {
            var creditCardViewModel = await creditCardService.GetByIdAsync(id);
            return View(creditCardViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCreditCardPost(int id)
        {
            CreditCardViewModel creditCardOwnerId = await creditCardService.GetByIdAsync(id);
            string ownerId = creditCardOwnerId.UserId;
            await creditCardService.DeleteAsync(id);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });
        }

    }
}
