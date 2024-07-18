using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;

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

                var tempData = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                var tempDataDictionary = tempData.GetTempData(context.HttpContext);

                tempDataDictionary["LoginAccessDenied"] = true;

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
