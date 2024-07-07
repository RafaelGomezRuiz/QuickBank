using AutoMapper;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Services
{
    public class PayService : IPayService
    {
        // Repos
        private readonly IBeneficeRepository beneficeRepository;
        private readonly ICreditCardRepository creditCardRepository;
        private readonly ILoanRepository loanRepository;
        private readonly ISavingAccountRepository savingAccountRepository;

        // Services
        private readonly ILogService logService;

        // Tools
        private readonly IMapper mapper;

        public PayService(
            IBeneficeRepository beneficeRepository,
            ICreditCardRepository creditCardRepository,
            ILoanRepository loanRepository,
            ISavingAccountRepository savingAccountRepository,
            ILogService logService,
            IMapper mapper
        )
        {
            this.beneficeRepository = beneficeRepository;
            this.creditCardRepository = creditCardRepository;
            this.loanRepository = loanRepository;
            this.savingAccountRepository = savingAccountRepository;
            this.logService = logService;
            this.mapper = mapper;
        }

        public async Task MakeExpressPay(ExpressPaySaveViewModel epsvm)
        {

            //// Validations of model
            //if (epsvm.Amount < BusinessLogicConstantsHelper.MinimumPaymentAmount)
            //{
            //    errorDictionary.Add("InvalidAmount", $"You must enter a valid amount, greater than {BusinessLogicConstantsHelper.MinimumPaymentAmount}");
            //}
            //if (epsvm.NumberAccountToPay != null && epsvm.NumberAccountToPay.Length > BusinessLogicConstantsHelper.MaxLengthNumberAccount)
            //{
            //    errorDictionary.Add("InvalidNumberCharacters", $"Invalid number of characters, minimun {BusinessLogicConstantsHelper.MaxLengthNumberAccount}");
            //}
            //if (errorDictionary.Count != 0) return errorDictionary;
        }
    }
}
