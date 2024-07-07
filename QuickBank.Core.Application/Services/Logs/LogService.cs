using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Logs;

namespace QuickBank.Core.Application.Services.Log
{
    public class LogService : ILogService
    {
        private readonly IPayLogRepository payLogRepository;
        private readonly ITransferLogRepository transferLogRepository;
        private readonly IMapper mapper;

        public LogService(
            IPayLogRepository payLogRepository,
            ITransferLogRepository transferLogRepository,
            IMapper mapper
        )
        {
            this.payLogRepository = payLogRepository;
            this.transferLogRepository = transferLogRepository;
            this.mapper = mapper;
        }
    }
}
