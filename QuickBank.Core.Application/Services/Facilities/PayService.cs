﻿using AutoMapper;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class PayService : IPayService
    {
        // Services
        private readonly IBeneficeService beneficeService;
        private readonly ICreditCardService creditCardService;
        private readonly ILoanService loanService;
        private readonly ISavingAccountService savingAccountService;
        private readonly IAccountService accountService;
        private readonly ILogService logService;

        // Tools
        private readonly IMapper mapper;

        public PayService(
            IBeneficeService beneficeService,
            ICreditCardService creditCardService,
            ILoanService loanService,
            ISavingAccountService savingAccountService,
            IAccountService accountService,
            ILogService logService,
            IMapper mapper
        )
        {
            this.beneficeService = beneficeService;
            this.creditCardService = creditCardService;
            this.loanService = loanService;
            this.savingAccountService = savingAccountService;
            this.accountService = accountService;
            this.logService = logService;
            this.mapper = mapper;
        }

        public async Task<ConfirmPayViewModel> GetPayConfirmation(string numberAccountToPay, string actionToConfirm)
        {
            var productToPay = await savingAccountService.GetViewModelByNumberAccountAsync(numberAccountToPay);
            var userFromProductToPay = await accountService.FindByIdAsync(productToPay.UserId);

            return new ConfirmPayViewModel()
            {
                UserFullName = $"{userFromProductToPay.FirstName} {userFromProductToPay.LastName}",
                PayToConfirm = actionToConfirm
            };
        }

        public async Task MakeExpressPay(ExpressPaySaveViewModel epsvm)
        {
            
        }
    }
}
