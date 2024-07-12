using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdministrationUserController : Controller
    {
        protected readonly IUserService userService;
        protected readonly ISavingAccountService savingAccountService;
        protected readonly ILoanService loanService;
        protected readonly ICreditCardService credictCardService;


        public AdministrationUserController(
            IUserService userService,
            ISavingAccountService savingAccountService,
            ILoanService loanService)
        {
            this.userService = userService;
            this.savingAccountService = savingAccountService;
            this.loanService = loanService;
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

                try
                {
                    await savingAccountService.SetSavingAccount(setSavingAccount);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, "No available saving accounts.");
                    return View("SaveUser", userSaveViewModel);
                }
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
            if (!ModelState.IsValid)
            {
                return View("SaveUser", userSaveViewModel);
            }
            var updateResult = await userService.UpdateUserAsync(userSaveViewModel);
            if (updateResult.HasError)
            {
                //Version que hacer
            }
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
            var userSavingAccounts = await savingAccountService.GetAllByUserIdAsync(ownerId);
            var userLoans = await loanService.GetAllByUserIdAsync(ownerId);
            //var userCredictCards = await credictCardService.GetAllByUserIdAsync(id);

            UserProductsViewModel userProducts = new()
            {
                OwnerId = ownerId,
                SavingAccounts = userSavingAccounts,
                Loans = userLoans,
            };
            return View(userProducts);
        }
        [HttpGet]
        public async Task<IActionResult> SetSavingAccount(string ownerId)
        {
            SetSavingAccount savingAccount = new()
            {
                UserId = ownerId,
            };
            //Mensaje que valide si se creo o no
            try
            {
                await savingAccountService.SetSavingAccount(savingAccount);
            }catch(Exception ex)
            {
                return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });
            }

            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });
        }
        [HttpGet]
        public async Task<IActionResult> SetLoan(string ownerId)
        {
            LoanSaveViewModel loanSaveViewModel = new()
            {
                OwnerId = ownerId,
            };
            return View(loanSaveViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetLoan(LoanSaveViewModel loanSaveViewModel)
        {
            await loanService.SetLoan(loanSaveViewModel);
            var userPrincipalSavingAccount = await savingAccountService.GetPrincipalSavingAccountAsync(loanSaveViewModel.OwnerId);
            userPrincipalSavingAccount.Balance += loanSaveViewModel.Amount;
            await savingAccountService.UpdateAsync(userPrincipalSavingAccount, userPrincipalSavingAccount.Id);
            return RedirectToRoute(new {Controller="AdministrationUser",Action="UserProducts",loanSaveViewModel.OwnerId});
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
            string ownerId = savingAccountOwner.UserId;
            await savingAccountService.DeleteAsync(id);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loanViewModel = await loanService.GetByIdAsync(id);
            return View(loanViewModel);
        }

        [HttpGet]
        //ojo con este nombre
        public async Task<IActionResult> DeleteLoanPost(int id)
        {
            var loanOwnerId = await loanService.GetByIdAsync(id);
            string ownerId =loanOwnerId.UserId;
            await loanService.DeleteAsync(id);
            return RedirectToRoute(new { Controller = "AdministrationUser", Action = "UserProducts", ownerId});
        }


    }
}
