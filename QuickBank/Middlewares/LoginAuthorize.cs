using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc.Filters;
using QuickBank.Controllers;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Helpers;

namespace QuickBank.Middlewares
{
    public class LoginAuthorize
    {
        protected readonly IUserHelper userHelper;
        public LoginAuthorize(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (userHelper.HasUser())
            {
                var controller = (AuthController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}
