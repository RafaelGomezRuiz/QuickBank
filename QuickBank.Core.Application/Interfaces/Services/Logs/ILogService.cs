﻿using QuickBank.Core.Domain.Entities.Logs;

namespace QuickBank.Core.Application.Interfaces.Services.Logs
{
    public interface ILogService
    {
        Task AddPayLogAsync(PayLogEntity entity);
        Task AddTransferLogAsync(TransferLogEntity entity);
        Task<List<PayLogEntity>> GetAllPayLogsAsync();
        Task<IEnumerable<PayLogEntity>> GetDailyPayLogsAsync();
        Task<List<TransferLogEntity>> GetAllTransferLogsAsync();
        Task<IEnumerable<TransferLogEntity>> GetDailyTransferLogsAsync();
    }
}
