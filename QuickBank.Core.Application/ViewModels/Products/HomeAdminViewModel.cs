using QuickBank.Core.Application.ViewModels.User;
using QuickBank.Core.Domain.Entities.Logs;

namespace QuickBank.Core.Application.ViewModels.Products
{
    public class HomeAdminViewModel
    {
        public int TransferLogs { get; set; }
        public int DailyTransferLogs { get; set; }
        public int PayLogs { get; set; }
        public int DailyPayLogs { get; set; }
        public int UsersActive { get; set; }
        public int UsersInactive { get; set; }
        public int ProductsAssigned { get; set; }

    }
}
