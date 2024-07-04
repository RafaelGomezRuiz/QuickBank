using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class BeneficeRepository : GenericRepository<BeneficeEntity>, IBeneficeRepository
    {
        private readonly ApplicationContext context;

        public BeneficeRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
