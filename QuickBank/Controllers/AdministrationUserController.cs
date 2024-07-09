﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.User;
using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdministrationUserController : Controller
    {
        protected readonly IUserService userService;
        public AdministrationUserController(IUserService userService)
        {
            this.userService = userService;
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
        public async Task<IActionResult> Add(UserSaveViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser",userViewModel);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response=await userService.RegisterAsync(userViewModel, origin);
            if (response.HasError)
            {
                userViewModel.HasError = response.HasError;
                userViewModel.ErrorDescription = response.ErrorDescription;
                return View(userViewModel);
            }
            return RedirectRoutesHelper.routeAdminHome;
        }
    }
}
