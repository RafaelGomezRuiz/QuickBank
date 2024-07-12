using AutoMapper;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Logs;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.ViewModels.Facilities;

namespace QuickBank.Core.Application.Services.Facilities
{
    public class FacilityService : IFacilityService
    {
        // Repos
        private readonly ICreditCardService creditCardService;
        private readonly ISavingAccountService savingAccountService;

        // Services
        private readonly ILogService logService;

        // Tools
        private readonly IMapper mapper;

        public FacilityService(
            ICreditCardService creditCardService,
            ISavingAccountService savingAccountService,
            ILogService logService,
            IMapper mapper
        )
        {
            this.creditCardService = creditCardService;
            this.savingAccountService = savingAccountService;
            this.logService = logService;
            this.mapper = mapper;
        }


        public async Task MakeCashAdvance(CashAdvancesSaveViewModel casvm)
        {
            // SavingAccount and credit card
            var creditCardFromPay = await creditCardService.GetByIdAsync(casvm.CreditCardId);
            var savingAccountToPay = await savingAccountService.GetByIdAsync(casvm.SavingAccountId);

            // Cedit chosen amount
            double interest = casvm.Amount * BusinessLogicConstantsHelper.CashAdvanceInterest;
            creditCardFromPay.Balance += casvm.Amount + interest;
            savingAccountToPay.Balance += casvm.Amount;

            // Update the products
            await creditCardService.UpdateAsync(creditCardFromPay, creditCardFromPay.Id);
            await savingAccountService.UpdateAsync(savingAccountToPay, savingAccountToPay.Id);
        }
    }
}
