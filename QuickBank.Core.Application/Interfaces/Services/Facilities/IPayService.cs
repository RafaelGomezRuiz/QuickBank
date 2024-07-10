using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IPayService
    {
        Task<ConfirmPayViewModel> GetExpressPayConfirmation(string numberAccountToPay);
        Task<ConfirmPayViewModel> GetCreditCardPayConfirmation(int creditCardId);
        Task MakeExpressPay(ExpressPaySaveViewModel epsvm);
        Task MakeCreditCardPay(CreditCardPaySaveViewModel ccsvm);
    }
}
