using AutoMapper;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Domain.Entities.Logs;
using System.Diagnostics;

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


        #region Payments Confirmations

        private ConfirmPayViewModel CreatePayConfirmation(string entityToBePaid, string payActionToConfirm)
        {
            return new ConfirmPayViewModel()
            {
                EntityToBePaid = entityToBePaid,
                PayActionToConfirm = payActionToConfirm
            };
        }

        public async Task<ConfirmPayViewModel> GetExpressPayConfirmation(string numberAccountToPay)
        {
            var savingAccountToPay = await savingAccountService.GetViewModelByNumberAccountAsync(numberAccountToPay);
            var userFromSavingAccountToPay = await accountService.FindByIdAsync(savingAccountToPay.UserId);

            return CreatePayConfirmation
            (
                $"{userFromSavingAccountToPay.FirstName} {userFromSavingAccountToPay.LastName}",
                "ConfirmExpressPay"
            );
        }

        public async Task<ConfirmPayViewModel> GetCreditCardPayConfirmation(int creditCardId)
        {
            var creditCard = await creditCardService.GetByIdAsync(creditCardId);

            return CreatePayConfirmation
            (
                creditCard.CardNumber,
                "ConfirmCreditCardPay"
            );
        }

        public async Task<ConfirmPayViewModel> GetLoanPayConfirmation(int loanId)
        {
            var loan = await loanService.GetByIdAsync(loanId);

            return CreatePayConfirmation
            (
                $"{loan.LoanNumber} - {loan.Description}",
                "ConfirmLoanPay"
            );
        }

        #endregion


        public async Task MakeExpressPay(ExpressPaySaveViewModel epsvm)
        {
            // SavingAccounts
            var savingAccountToPay = await savingAccountService.GetViewModelByNumberAccountAsync(epsvm.NumberAccountToPay);
            var savingAccountFromPay = await savingAccountService.GetByIdAsync(epsvm.AccountIdFromPay);

            // Debit chosen amount
            savingAccountFromPay.Balance -= epsvm.Amount;
            await savingAccountService.UpdateAsync(savingAccountFromPay, savingAccountFromPay.Id);

            // Credit chosen amount
            savingAccountToPay.Balance += epsvm.Amount;
            await savingAccountService.UpdateAsync(savingAccountToPay, savingAccountToPay.Id);

            // Log the pay
            var payLog = new PayLogEntity() { CreationDate = DateTime.Now };
            await logService.AddPayLogAsync(payLog);
        }


        public async Task MakeCreditCardPay(CreditCardPaySaveViewModel ccpsvm)
        {
            // SavingAccount and credit card
            var creditCardToPay = await creditCardService.GetByIdAsync(ccpsvm.CreditCardIdToPay);
            var savingAccountFromPay = await savingAccountService.GetByIdAsync(ccpsvm.SavingAccountIdFromPay);

            // Debit chosen amount
            savingAccountFromPay.Balance -= ccpsvm.Amount;
            creditCardToPay.Balance -= ccpsvm.Amount;

            // Card debit excess
            bool excessDebit = creditCardToPay.Balance < 0;
            if (excessDebit)
            {
                savingAccountFromPay.Balance += Math.Abs(creditCardToPay.Balance);
                creditCardToPay.Balance = 0.0;
            }

            // Update the products
            await savingAccountService.UpdateAsync(savingAccountFromPay, savingAccountFromPay.Id);
            await creditCardService.UpdateAsync(creditCardToPay, creditCardToPay.Id);

            // Log the pay
            var payLog = new PayLogEntity() { CreationDate = DateTime.Now };
            await logService.AddPayLogAsync(payLog);
        }

        public async Task MakeLoanPay(LoanPaySaveViewModel lpsvm)
        {
            // SavingAccount and credit card
            var loanToPay = await loanService.GetByIdAsync(lpsvm.LoanIdToPay);
            var savingAccountFromPay = await savingAccountService.GetByIdAsync(lpsvm.SavingAccountIdFromPay);

            // Debit chosen amount
            savingAccountFromPay.Balance -= lpsvm.Amount;
            loanToPay.Amount -= lpsvm.Amount;

            // Loan debit excess
            bool excessDebit = loanToPay.Amount < 0;
            if (excessDebit)
            {
                savingAccountFromPay.Balance += Math.Abs(loanToPay.Amount);
                loanToPay.Amount = 0.0;
            }

            // Update the products
            await savingAccountService.UpdateAsync(savingAccountFromPay, savingAccountFromPay.Id);
            await loanService.UpdateAsync(loanToPay, loanToPay.Id);

            // Log the pay
            var payLog = new PayLogEntity() { CreationDate = DateTime.Now };
            await logService.AddPayLogAsync(payLog);
        }

    }
}
