using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.ViewModels.Products;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC, ADMIN")]
    public class HomeController : Controller
    {
        private readonly ISavingAccountService savingAccountService;
        private readonly ICreditCardService creditCardService;
        private readonly ILoanService loanService;
        private readonly ILogService logService;
        private readonly IUserService userService;
        private readonly IProductService productService;


        private readonly IUserHelper userHelper;

        public HomeController(
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            ILoanService loanService,
            IUserHelper userHelper,
            ILogService logService,
            IUserService userService,
            IProductService productService)
        {
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.loanService = loanService;
            this.userHelper = userHelper;
            this.logService = logService;
            this.userService = userService;
            this.productService = productService;

        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AdminHome()
        {
            if (HttpContext.Items.ContainsKey("NotificationFromRedirectioned"))
            {
                TempData["NotificationFromRedirectioned"] = HttpContext.Items["NotificationFromRedirectioned"] as string;
            }
            HomeAdminViewModel homeAdminViewModel = new()
            {
                PayLogs = (await logService.GetAllPayLogsAsync()).Count,
                DailyPayLogs = (await logService.GetDailyPayLogsAsync()).Count(),
                TransferLogs = (await logService.GetAllTransferLogsAsync()).Count(),
                DailyTransferLogs = (await logService.GetDailyTransferLogsAsync()).Count(),
                UsersActive = (await userService.GetActiveUsersAsync()).Count(),
                UsersInactive = (await userService.GetInactiveUsersAsync()).Count(),
                ProductsAssigned = await productService.GetAssignedProducts()
            };
            return View(homeAdminViewModel);
        }


        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> BasicHome()
        {
            var user = userHelper.GetUser();
            var hbvm = new HomeBasicViewModel
            {
                SavingAccounts = await savingAccountService.GetAllByUserIdAsync(user.Id),
                CreditCards = await creditCardService.GetAllByUserIdAsync(user.Id),
                Loans = await loanService.GetAllByUserIdAsync(user.Id)
            };

            //if (HttpContext.Items.ContainsKey("NotificationFromRedirectioned"))
            //{
            //    TempData["NotificationFromRedirectioned"] = HttpContext.Items["NotificationFromRedirectioned"] as string;
            //}

            return View(hbvm);
        }
    }
}
