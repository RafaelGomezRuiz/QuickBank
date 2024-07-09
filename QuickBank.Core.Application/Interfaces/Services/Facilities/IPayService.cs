using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IPayService
    {
        Task<ConfirmPayViewModel> GetPayConfirmation(string numberAccountToPay, string actionToConfirm);
        Task MakeExpressPay(ExpressPaySaveViewModel epsvm);
    }
}
