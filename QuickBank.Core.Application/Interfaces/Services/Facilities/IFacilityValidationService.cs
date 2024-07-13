using QuickBank.Core.Application.ViewModels.Facilities;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IFacilityValidationService
    {
        Task<Dictionary<string, string>> TransferValidation(TransferSaveViewModel tsvm);
        Task<Dictionary<string, string>> CashAdvanceValidation(CashAdvancesSaveViewModel casvm);
        Task<Dictionary<string, string>> ValidateBeneficiary(BeneficeSaveViewModel model);
    }
}
