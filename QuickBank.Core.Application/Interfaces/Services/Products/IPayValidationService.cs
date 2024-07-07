using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface IPayValidationService
    {
        Task<Dictionary<string, string>> ExpressPayValidation(ExpressPaySaveViewModel epsvm);
    }
}
