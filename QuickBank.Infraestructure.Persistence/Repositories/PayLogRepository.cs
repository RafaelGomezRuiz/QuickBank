using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities.Logs;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class PayLogRepository : GenericRepository<PayLogEntity>, IPayLogRepository
    {
        private readonly ApplicationContext context;

        public PayLogRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
