using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.User;
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
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HasErrors = true;
                return View(loginVm);
            }

            AuthenticationResponse responseLogin = await userService.LoginAsync(loginVm);
            if (responseLogin != null && responseLogin.HasError != true)
            {
                userHelper.SetUser(responseLogin);
                string principalRole = responseLogin.Roles![^1];

                switch (principalRole)
                {
                    case nameof(ERoles.BASIC): return RedirectRoutesHelper.routeBasicHome;
                    case nameof(ERoles.ADMIN): return RedirectRoutesHelper.routeAdminHome;
                    default: return RedirectRoutesHelper.routeUndefiniedHome;
                }
            }
            else
            {
                loginVm.HasError = true;
                loginVm.ErrorDescription = responseLogin.ErrorDescription;
                ViewBag.HasErrors = true;
                return View(loginVm);
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await userService.SignOutAsync();
            userHelper.RemoveUser();
            return RedirectToRoute(new { controller = "Auth", action = "Login" });
        }
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
