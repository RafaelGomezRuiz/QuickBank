﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickBank.Infrastructure.Persistence.Contexts;

#nullable disable

namespace QuickBank.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240705025926_AddSeeding")]
    partial class AddSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.BeneficeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BenefitedSavingAccountId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BenefitedSavingAccountId");

                    b.ToTable("Benefices", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BenefitedSavingAccountId = 2,
                            OwnerId = "f294u-ewrdm-woj93-hj3dn-8937w"
                        });
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Logs.PayLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PayLogs", (string)null);
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Logs.TransferLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TransferLogs", (string)null);
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Productos.CreditCardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("LimitCredit")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreditCards", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 1500.0,
                            CardNumber = "123456789",
                            CreationDate = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 5000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            Balance = 2500.0,
                            CardNumber = "234567890",
                            CreationDate = new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 7000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            Balance = 3000.0,
                            CardNumber = "345678901",
                            CreationDate = new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 8000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 4,
                            Balance = 1200.0,
                            CardNumber = "456789012",
                            CreationDate = new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 6000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 5,
                            Balance = 500.0,
                            CardNumber = "567890123",
                            CreationDate = new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 4000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 6,
                            Balance = 1000.0,
                            CardNumber = "678901234",
                            CreationDate = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 4500.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 7,
                            Balance = 2000.0,
                            CardNumber = "789012345",
                            CreationDate = new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 5500.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 8,
                            Balance = 3500.0,
                            CardNumber = "890123456",
                            CreationDate = new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 9000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 9,
                            Balance = 500.0,
                            CardNumber = "901234567",
                            CreationDate = new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 3000.0,
                            Status = 0
                        },
                        new
                        {
                            Id = 10,
                            Balance = 750.0,
                            CardNumber = "012345678",
                            CreationDate = new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExpirationDate = new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LimitCredit = 3500.0,
                            Status = 0
                        });
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Productos.LoanEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<string>("LoanNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Loans", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50000.0,
                            ApplicationDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Car loan",
                            InterestRate = 5.0,
                            LoanNumber = "LN000001",
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            Amount = 75000.0,
                            ApplicationDate = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Home loan",
                            InterestRate = 4.5,
                            LoanNumber = "LN000002",
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            Amount = 20000.0,
                            ApplicationDate = new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Personal loan",
                            InterestRate = 6.0,
                            LoanNumber = "LN000003",
                            Status = 0
                        },
                        new
                        {
                            Id = 4,
                            Amount = 30000.0,
                            ApplicationDate = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Education loan",
                            InterestRate = 5.5,
                            LoanNumber = "LN000004",
                            Status = 0
                        },
                        new
                        {
                            Id = 5,
                            Amount = 15000.0,
                            ApplicationDate = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Medical loan",
                            InterestRate = 4.0,
                            LoanNumber = "LN000005",
                            Status = 0
                        },
                        new
                        {
                            Id = 6,
                            Amount = 100000.0,
                            ApplicationDate = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Business loan",
                            InterestRate = 3.5,
                            LoanNumber = "LN000006",
                            Status = 0
                        },
                        new
                        {
                            Id = 7,
                            Amount = 45000.0,
                            ApplicationDate = new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Vacation loan",
                            InterestRate = 5.0,
                            LoanNumber = "LN000007",
                            Status = 0
                        },
                        new
                        {
                            Id = 8,
                            Amount = 60000.0,
                            ApplicationDate = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Renovation loan",
                            InterestRate = 4.7999999999999998,
                            LoanNumber = "LN000008",
                            Status = 0
                        },
                        new
                        {
                            Id = 9,
                            Amount = 80000.0,
                            ApplicationDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Investment loan",
                            InterestRate = 4.2000000000000002,
                            LoanNumber = "LN000009",
                            Status = 0
                        },
                        new
                        {
                            Id = 10,
                            Amount = 55000.0,
                            ApplicationDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AprovalDate = new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deadline = new DateTime(2026, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Consolidation loan",
                            InterestRate = 5.2999999999999998,
                            LoanNumber = "LN000010",
                            Status = 0
                        });
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Productos.SavingAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Principal")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SavingAccounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountNumber = "SAV000001",
                            Balance = 15000.0,
                            CreationDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            AccountNumber = "SAV000002",
                            Balance = 25000.0,
                            CreationDate = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            AccountNumber = "SAV000003",
                            Balance = 18000.0,
                            CreationDate = new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 4,
                            AccountNumber = "SAV000004",
                            Balance = 30000.0,
                            CreationDate = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 5,
                            AccountNumber = "SAV000005",
                            Balance = 20000.0,
                            CreationDate = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 6,
                            AccountNumber = "SAV000006",
                            Balance = 35000.0,
                            CreationDate = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 7,
                            AccountNumber = "SAV000007",
                            Balance = 27000.0,
                            CreationDate = new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 8,
                            AccountNumber = "SAV000008",
                            Balance = 40000.0,
                            CreationDate = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 9,
                            AccountNumber = "SAV000009",
                            Balance = 22000.0,
                            CreationDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        },
                        new
                        {
                            Id = 10,
                            AccountNumber = "SAV000010",
                            Balance = 28000.0,
                            CreationDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Principal = false,
                            Status = 0
                        });
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.BeneficeEntity", b =>
                {
                    b.HasOne("QuickBank.Core.Domain.Entities.Productos.SavingAccountEntity", "BenefitedSavingAccount")
                        .WithMany("Benefices")
                        .HasForeignKey("BenefitedSavingAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BenefitedSavingAccount");
                });

            modelBuilder.Entity("QuickBank.Core.Domain.Entities.Productos.SavingAccountEntity", b =>
                {
                    b.Navigation("Benefices");
                });
#pragma warning restore 612, 618
        }
    }
}
