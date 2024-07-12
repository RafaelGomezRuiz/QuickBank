using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IPayService
    {
        Task<ConfirmPayViewModel> GetExpressPayConfirmation(string numberAccountToPay);
        Task<ConfirmPayViewModel> GetCreditCardPayConfirmation(int creditCardId);
        Task<ConfirmPayViewModel> GetLoanPayConfirmation(int loanId);
        Task<ConfirmPayViewModel> GetBeneficiaryPayConfirmation(int beneficeId);
        Task MakeExpressPay(ExpressPaySaveViewModel epsvm);
        Task MakeCreditCardPay(CreditCardPaySaveViewModel ccpsvm);
        Task MakeLoanPay(LoanPaySaveViewModel lpsvm);
        Task MakeBeneficiaryPay(BeneficiaryPaySaveViewModel bpsvm);
    }
}
