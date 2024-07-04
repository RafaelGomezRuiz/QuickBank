using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities.Logs;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class TransferLogRepository : GenericRepository<TransferLogEntity>, ITransferLogRepository
    {
        private readonly ApplicationContext context;

        public TransferLogRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
