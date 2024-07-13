using QuickBank.Core.Application.ViewModels.Facilities;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IFacilityService
    {
        Task MakeTransfer(TransferSaveViewModel tsvm);
        Task MakeCashAdvance(CashAdvancesSaveViewModel casvm);
    }
}
