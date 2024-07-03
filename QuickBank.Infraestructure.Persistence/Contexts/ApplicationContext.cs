using Microsoft.EntityFrameworkCore;
using QuickBank.Core.Domain.Entities;
using QuickBank.Core.Domain.Entities.Logs;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<BeneficeEntity> Benefices { get; set; }
        public DbSet<CreditCardEntity> CreditCards { get; set; }
        public DbSet<LoanEntity> Loans { get; set; }
        public DbSet<SavingAccountEntity> SavingAccounts { get; set; }
        public DbSet<PayLogEntity> PayLogs { get; set; }
        public DbSet<TransferLogEntity> TransferLogs { get; set; }


        #region MaybeForDelete

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = "Default";
        //                entry.Entity.CreatedTime = DateTime.Now;
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastUpdatedBy = "Default";
        //                entry.Entity.LastUpdatedTime = DateTime.Now;
        //                break;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Tables
            modelBuilder.Entity<BeneficeEntity>().ToTable("Benefices");
            modelBuilder.Entity<CreditCardEntity>().ToTable("CreditCards");
            modelBuilder.Entity<LoanEntity>().ToTable("Loans");
            modelBuilder.Entity<SavingAccountEntity>().ToTable("SavingAccounts");
            modelBuilder.Entity<PayLogEntity>().ToTable("PayLogs");
            modelBuilder.Entity<TransferLogEntity>().ToTable("TransferLogs");
            #endregion


            #region Primary Keys
            modelBuilder.Entity<BeneficeEntity>().HasKey(b => b.Id);
            modelBuilder.Entity<CreditCardEntity>().HasKey(cc => cc.Id);
            modelBuilder.Entity<LoanEntity>().HasKey(l => l.Id);
            modelBuilder.Entity<SavingAccountEntity>().HasKey(sa => sa.Id);
            modelBuilder.Entity<PayLogEntity>().HasKey(pl => pl.Id);
            modelBuilder.Entity<TransferLogEntity>().HasKey(tl => tl.Id);
            #endregion


            #region Relationship of entities
            modelBuilder.Entity<SavingAccountEntity>()
                .HasMany(sa => sa.Benefices)
                .WithOne(b => b.BenefitedSavingAccount)
                .HasForeignKey(b => b.BenefitedSavingAccountId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion


            #region Tables Properties


            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
