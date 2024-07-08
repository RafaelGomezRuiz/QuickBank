using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Services.Products;
using QuickBank.Core.Application.ViewModels.Products;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC, ADMIN")]
    public class HomeController : Controller
    {
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly ILoanService loanService;

        private readonly IUserHelper userHelper;

        public HomeController(
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            ILoanService loanService,
            IUserHelper userHelper)
        {
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.loanService = loanService;
            this.userHelper = userHelper;
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult AdminHome()
        {
            if (HttpContext.Items.ContainsKey("NotificationFromRedirectioned"))
            {
                TempData["NotificationFromRedirectioned"] = HttpContext.Items["NotificationFromRedirectioned"] as string;
            }
            return View();
        }


        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> BasicHome()
        {
            var user = userHelper.GetUser();
            var model = new HomeBasicViewModel
            {
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(user.Id),
                CreditCards = await creditCardService.GetAllByUserIdAsync(user.Id),
                Loans = await loanService.GetAllByUserIdAsync(user.Id)
            };

            if (HttpContext.Items.ContainsKey("NotificationFromRedirectioned"))
            {
                TempData["NotificationFromRedirectioned"] = HttpContext.Items["NotificationFromRedirectioned"] as string;
            }

            return View(model);
        }
    }
}
