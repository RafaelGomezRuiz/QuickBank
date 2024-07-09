using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.User;
using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdministrationUserController : Controller
    {
        protected readonly IUserService userService;
        protected readonly ISavingAccountService savingAccountService;
        public AdministrationUserController(IUserService userService, ISavingAccountService savingAccountService)
        {
            this.userService = userService;
            this.savingAccountService = savingAccountService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetAllAsync());
        }
        public async Task<IActionResult> Add()
        {
            return View("SaveUser",new UserSaveViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserSaveViewModel userSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser",userSaveViewModel);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response=await userService.RegisterAsync(userSaveViewModel, origin);
            if (response.HasError)
            {
                userSaveViewModel.HasError = response.HasError;
                userSaveViewModel.ErrorDescription = response.ErrorDescription;
                return View("SaveUser", userSaveViewModel);
            }
            SetSavingAccount setSavingAccount = new();
            setSavingAccount.UserId = response.Id;
            setSavingAccount.InitialAmount = userSaveViewModel.InitialAmount;
            await savingAccountService.SetSavingAccount(setSavingAccount);
            
            return RedirectRoutesHelper.routeAdminHome;
        }
    }
}
