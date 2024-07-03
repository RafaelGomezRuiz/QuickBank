using Microsoft.EntityFrameworkCore;
using QuickBank.Core.Domain.Commons;

namespace QuickBank.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Default";
                        entry.Entity.CreatedTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedBy = "Default";
                        entry.Entity.LastUpdatedTime = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Tables
            

            #endregion


            #region Primary Keys
            

            #endregion


            #region Relationship of entities
            

            #endregion


            #region Tables Properties
 

            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
