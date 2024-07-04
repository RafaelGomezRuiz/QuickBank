using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities.Productos;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class SavingAccountRepository : GenericRepository<SavingAccountEntity>, ISavingAccountRepository
    {
        private readonly ApplicationContext context;

        public SavingAccountRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
