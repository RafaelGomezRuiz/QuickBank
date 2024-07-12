using QuickBank.Core.Application.ViewModels.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IFacilityValidationService
    {
        Task<Dictionary<string, string>> ValidateTransfer(TransferSaveViewModel model);
        Task<Dictionary<string, string>> CashAdvanceValidation(CashAdvancesSaveViewModel casvm);
    }
}
