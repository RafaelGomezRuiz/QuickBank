using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services.Products
{
    public interface IPayValidationService
    {
        Task<Dictionary<string, string>> ExpressPayValidation(ExpressPaySaveViewModel epsvm);
        Task<Dictionary<string, string>> CreditCardPayValidation(CreditCardPaySaveViewModel ccpsvm);
        Task<Dictionary<string, string>> LoanPayValidation(LoanPaySaveViewModel lpsvm);
        Task<Dictionary<string, string>> BeneficiaryPayValidation(BeneficiaryPaySaveViewModel bpsvm);
    }
}
