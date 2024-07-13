using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
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
        private readonly ILogService logService;
        private readonly IUserService userService;

        private readonly IUserHelper userHelper;

        public HomeController(
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            ILoanService loanService,
            IUserHelper userHelper,
            ILogService logService,
            IUserService userService)
        {
            this.savingAccountService = savingAccountService;
            this.creditCardService = creditCardService;
            this.loanService = loanService;
            this.userHelper = userHelper;
            this.logService = logService;
            this.userService = userService;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AdminHome()
        {
            if (HttpContext.Items.ContainsKey("NotificationFromRedirectioned"))
            {
                TempData["NotificationFromRedirectioned"] = HttpContext.Items["NotificationFromRedirectioned"] as string;
            }
            var payLogs = await logService.GetAllPayLogsAsync();
            var dailyPayLogs= await logService.GetDailyPayLogsAsync();
            var transferLogs = await logService.GetAllTransferLogsAsync();
            var dailyTransferLogs = await logService.GetDailyTransferLogsAsync();
            var totalUsers = await userService.GetAllAsync();
            HomeAdminViewModel homeAdminViewModel = new()
            {
                PayLogs = payLogs.Count,
                TransferLogs = transferLogs.Count,
                DailyPayLogs=dailyPayLogs.Count(),
                DailyTransferLogs=dailyTransferLogs.Count(),

            };
            return View(homeAdminViewModel);
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
