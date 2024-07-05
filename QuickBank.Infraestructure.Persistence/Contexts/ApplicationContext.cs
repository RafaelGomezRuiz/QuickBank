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

            base.OnModelCreating(modelBuilder);

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


            #region Seedings

            //Product
            modelBuilder.Entity<CreditCardEntity>().HasData(
                new CreditCardEntity { Id = 1, CardNumber = "123456789", Balance = 1500.00, LimitCredit = 5000.00, Status = 0, CreationDate = DateTime.Parse("2023-01-15"), ExpirationDate = DateTime.Parse("2026-01-15") },
                new CreditCardEntity { Id = 2, CardNumber = "234567890", Balance = 2500.00, LimitCredit = 7000.00, Status = 0, CreationDate = DateTime.Parse("2023-02-10"), ExpirationDate = DateTime.Parse("2026-02-10") },
                new CreditCardEntity { Id = 3, CardNumber = "345678901", Balance = 3000.00, LimitCredit = 8000.00, Status = 0, CreationDate = DateTime.Parse("2023-03-05"), ExpirationDate = DateTime.Parse("2026-03-05") },
                new CreditCardEntity { Id = 4, CardNumber = "456789012", Balance = 1200.00, LimitCredit = 6000.00, Status = 0, CreationDate = DateTime.Parse("2023-04-20"), ExpirationDate = DateTime.Parse("2026-04-20") },
                new CreditCardEntity { Id = 5, CardNumber = "567890123", Balance = 500.00, LimitCredit = 4000.00, Status = 0, CreationDate = DateTime.Parse("2023-05-30"), ExpirationDate = DateTime.Parse("2026-05-30") },
                new CreditCardEntity { Id = 6, CardNumber = "678901234", Balance = 1000.00, LimitCredit = 4500.00, Status = 0, CreationDate = DateTime.Parse("2023-06-01"), ExpirationDate = DateTime.Parse("2026-06-01") },
                new CreditCardEntity { Id = 7, CardNumber = "789012345", Balance = 2000.00, LimitCredit = 5500.00, Status = 0, CreationDate = DateTime.Parse("2023-07-10"), ExpirationDate = DateTime.Parse("2026-07-10") },
                new CreditCardEntity { Id = 8, CardNumber = "890123456", Balance = 3500.00, LimitCredit = 9000.00, Status = 0, CreationDate = DateTime.Parse("2023-08-20"), ExpirationDate = DateTime.Parse("2026-08-20") },
                new CreditCardEntity { Id = 9, CardNumber = "901234567", Balance = 500.00, LimitCredit = 3000.00, Status = 0, CreationDate = DateTime.Parse("2023-09-15"), ExpirationDate = DateTime.Parse("2026-09-15") },
                new CreditCardEntity { Id = 10, CardNumber = "012345678", Balance = 750.00, LimitCredit = 3500.00, Status = 0, CreationDate = DateTime.Parse("2023-10-25"), ExpirationDate = DateTime.Parse("2026-10-25") }
            );

            //Loan  
            modelBuilder.Entity<LoanEntity>().HasData(
                new LoanEntity { Id = 1, LoanNumber = "LN000001", Amount = 50000.00, Deadline = DateTime.Parse("2025-01-15"), InterestRate = 5.0, Description = "Car loan", Status = 0, ApplicationDate = DateTime.Parse("2023-01-01"), AprovalDate = DateTime.Parse("2023-01-10") },
                new LoanEntity { Id = 2, LoanNumber = "LN000002", Amount = 75000.00, Deadline = DateTime.Parse("2026-02-20"), InterestRate = 4.5, Description = "Home loan", Status = 0, ApplicationDate = DateTime.Parse("2023-02-01"), AprovalDate = DateTime.Parse("2023-02-15") },
                new LoanEntity { Id = 3, LoanNumber = "LN000003", Amount = 20000.00, Deadline = DateTime.Parse("2024-03-10"), InterestRate = 6.0, Description = "Personal loan", Status = 0, ApplicationDate = DateTime.Parse("2023-03-01"), AprovalDate = DateTime.Parse("2023-03-05") },
                new LoanEntity { Id = 4, LoanNumber = "LN000004", Amount = 30000.00, Deadline = DateTime.Parse("2025-04-25"), InterestRate = 5.5, Description = "Education loan", Status = 0, ApplicationDate = DateTime.Parse("2023-04-01"), AprovalDate = DateTime.Parse("2023-04-10") },
                new LoanEntity { Id = 5, LoanNumber = "LN000005", Amount = 15000.00, Deadline = DateTime.Parse("2024-05-30"), InterestRate = 4.0, Description = "Medical loan", Status = 0, ApplicationDate = DateTime.Parse("2023-05-01"), AprovalDate = DateTime.Parse("2023-05-05") },
                new LoanEntity { Id = 6, LoanNumber = "LN000006", Amount = 100000.00, Deadline = DateTime.Parse("2026-06-15"), InterestRate = 3.5, Description = "Business loan", Status = 0, ApplicationDate = DateTime.Parse("2023-06-01"), AprovalDate = DateTime.Parse("2023-06-10") },
                new LoanEntity { Id = 7, LoanNumber = "LN000007", Amount = 45000.00, Deadline = DateTime.Parse("2025-07-20"), InterestRate = 5.0, Description = "Vacation loan", Status = 0, ApplicationDate = DateTime.Parse("2023-07-01"), AprovalDate = DateTime.Parse("2023-07-05") },
                new LoanEntity { Id = 8, LoanNumber = "LN000008", Amount = 60000.00, Deadline = DateTime.Parse("2026-08-25"), InterestRate = 4.8, Description = "Renovation loan", Status = 0, ApplicationDate = DateTime.Parse("2023-08-01"), AprovalDate = DateTime.Parse("2023-08-10") },
                new LoanEntity { Id = 9, LoanNumber = "LN000009", Amount = 80000.00, Deadline = DateTime.Parse("2025-09-15"), InterestRate = 4.2, Description = "Investment loan", Status = 0, ApplicationDate = DateTime.Parse("2023-09-01"), AprovalDate = DateTime.Parse("2023-09-05") },
                new LoanEntity { Id = 10, LoanNumber = "LN000010", Amount = 55000.00, Deadline = DateTime.Parse("2026-10-30"), InterestRate = 5.3, Description = "Consolidation loan", Status = 0, ApplicationDate = DateTime.Parse("2023-10-01"), AprovalDate = DateTime.Parse("2023-10-05") }
            );

            //SavingAccount
            modelBuilder.Entity<SavingAccountEntity>().HasData(
                new SavingAccountEntity { Id = 1, CreationDate = DateTime.Parse("2023-01-01"), AccountNumber = "SAV000001", Balance = 15000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 2, CreationDate = DateTime.Parse("2023-02-01"), AccountNumber = "SAV000002", Balance = 25000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 3, CreationDate = DateTime.Parse("2023-03-01"), AccountNumber = "SAV000003", Balance = 18000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 4, CreationDate = DateTime.Parse("2023-04-01"), AccountNumber = "SAV000004", Balance = 30000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 5, CreationDate = DateTime.Parse("2023-05-01"), AccountNumber = "SAV000005", Balance = 20000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 6, CreationDate = DateTime.Parse("2023-06-01"), AccountNumber = "SAV000006", Balance = 35000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 7, CreationDate = DateTime.Parse("2023-07-01"), AccountNumber = "SAV000007", Balance = 27000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 8, CreationDate = DateTime.Parse("2023-08-01"), AccountNumber = "SAV000008", Balance = 40000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 9, CreationDate = DateTime.Parse("2023-09-01"), AccountNumber = "SAV000009", Balance = 22000.00, Status = 0, Principal = false },
                new SavingAccountEntity { Id = 10, CreationDate = DateTime.Parse("2023-10-01"), AccountNumber = "SAV000010", Balance = 28000.00, Status = 0, Principal = false }
            );

            //Benefice
            modelBuilder.Entity<BeneficeEntity>().HasData(
                new BeneficeEntity { Id = 1, OwnerId = "f294u-ewrdm-woj93-hj3dn-8937w", BenefitedSavingAccountId = 2 }
            );
            #endregion

        }
    }
}
