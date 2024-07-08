using Microsoft.AspNetCore.Mvc.Filters;
using QuickBank.Controllers;
using QuickBank.Core.Application.Interfaces.Helpers;
using System.Net.NetworkInformation;

using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Dtos.Account;
using Org.BouncyCastle.X509;
using QuickBank.Core.Application.Helpers;

namespace QuickBank.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        protected readonly IUserHelper userHelper;
        private readonly AuthenticationResponse userLogged;
        string? principalUserRol;

        public LoginAuthorize(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            userLogged = userHelper.GetUser();
            principalUserRol = userLogged?.Roles![^1];
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (userHelper.HasUser())
            {
                var controller = (AuthController)context.Controller;
                controller.TempData["NotificationFromRedirectioned"] = "You are already logged in, you cant go to the login, and have been redirected to the home page.";
                switch (principalUserRol)
                {
                    case nameof(ERoles.BASIC): context.Result = RedirectRoutesHelper.routeBasicHome; break;
                    case nameof(ERoles.ADMIN): context.Result = RedirectRoutesHelper.routeAdminHome; break;
                }
            }
            else
            {
                await next();
            }
        }
    }
}
