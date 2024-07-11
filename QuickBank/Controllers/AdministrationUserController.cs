﻿using Microsoft.AspNetCore.Authorization;
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
            UserSaveViewModel user=await userService.FindyByIdAsync(id);
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
            return View("SaveUser",user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserSaveViewModel userSaveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser", userSaveViewModel);
            }
            await userService.UpdateUserAsync(userSaveViewModel);
            SavingAccountViewModel savingAccountViewModel=await savingAccountService.GetPrincipalSavingAccountAsync(userSaveViewModel.Id);
            savingAccountViewModel.Balance += (double)userSaveViewModel.InitialAmount;
            savingAccountService.UpdateAsync(savingAccountViewModel,savingAccountViewModel.Id);
            return RedirectRoutesHelper.routeAdmininistrationUserIndex;
        }
    }
}
