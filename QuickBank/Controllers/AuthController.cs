﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Auth;
using QuickBank.Middlewares;

namespace QuickBank.Controllers
{
    public class AuthController : Controller
    {
        protected readonly IUserService userService;
        protected readonly IUserHelper userHelper;

        public AuthController(IUserService userService, IUserHelper userHelper)
        {
            this.userService = userService;
            this.userHelper = userHelper;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            AuthenticationResponse responseLogin = await userService.LoginAsync(loginVm);
            if (responseLogin != null && responseLogin.HasError != true)
            {
                userHelper.SetUser(responseLogin);

                string actionToReditect;
                string principalRole = responseLogin.Roles![^1];

                switch (principalRole)
                {
                    case nameof(ERoles.BASIC): actionToReditect = "BasicHome"; break;
                    case nameof(ERoles.ADMIN): actionToReditect = "AdminHome"; break;
                    default: actionToReditect = "UNDEFINIED-ROLE"; break;
                }

                return RedirectToRoute(new { controller = "Home", action = actionToReditect });
            }
            return View(loginVm);
        }
        public async Task<IActionResult> SignOut()
        {
            await userService.SignOutAsync();  
            userHelper.RemoveUser();
            return RedirectToRoute(new {controller="Auth",action="Index"});
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
