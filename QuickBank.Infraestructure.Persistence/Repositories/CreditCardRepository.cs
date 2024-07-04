using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities.Productos;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class CreditCardRepository : GenericRepository<CreditCardEntity>, ICreditCardRepository
    {
        private readonly ApplicationContext context;

        public CreditCardRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
