using QuickBank.Core.Domain.Entities.Logs;

namespace QuickBank.Core.Application.Interfaces.Services.Logs
{
    public interface ILogService
    {
        Task AddPayLogAsync(PayLogEntity entity);
        Task<List<PayLogEntity>> GetAllPayLogsAsync();
    }
}
