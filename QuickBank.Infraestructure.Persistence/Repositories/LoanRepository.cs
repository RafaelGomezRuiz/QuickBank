using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Domain.Entities.Productos;
using QuickBank.Infrastructure.Persistence.Contexts;

namespace QuickBank.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<LoanEntity>, ILoanRepository
    {
        private readonly ApplicationContext context;

        public LoanRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
