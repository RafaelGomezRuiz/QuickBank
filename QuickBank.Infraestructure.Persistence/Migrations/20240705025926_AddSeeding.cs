using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuickBank.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "Balance", "CardNumber", "CreationDate", "ExpirationDate", "LimitCredit", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1500.0, "123456789", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000.0, 0, null },
                    { 2, 2500.0, "234567890", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000.0, 0, null },
                    { 3, 3000.0, "345678901", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000.0, 0, null },
                    { 4, 1200.0, "456789012", new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6000.0, 0, null },
                    { 5, 500.0, "567890123", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000.0, 0, null },
                    { 6, 1000.0, "678901234", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4500.0, 0, null },
                    { 7, 2000.0, "789012345", new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5500.0, 0, null },
                    { 8, 3500.0, "890123456", new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9000.0, 0, null },
                    { 9, 500.0, "901234567", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000.0, 0, null },
                    { 10, 750.0, "012345678", new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3500.0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "ApplicationDate", "AprovalDate", "Deadline", "Description", "InterestRate", "LoanNumber", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 50000.0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Car loan", 5.0, "LN000001", 0, null },
                    { 2, 75000.0, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home loan", 4.5, "LN000002", 0, null },
                    { 3, 20000.0, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Personal loan", 6.0, "LN000003", 0, null },
                    { 4, 30000.0, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Education loan", 5.5, "LN000004", 0, null },
                    { 5, 15000.0, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medical loan", 4.0, "LN000005", 0, null },
                    { 6, 100000.0, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business loan", 3.5, "LN000006", 0, null },
                    { 7, 45000.0, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vacation loan", 5.0, "LN000007", 0, null },
                    { 8, 60000.0, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Renovation loan", 4.7999999999999998, "LN000008", 0, null },
                    { 9, 80000.0, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Investment loan", 4.2000000000000002, "LN000009", 0, null },
                    { 10, 55000.0, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consolidation loan", 5.2999999999999998, "LN000010", 0, null }
                });

            migrationBuilder.InsertData(
                table: "SavingAccounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CreationDate", "Principal", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "SAV000001", 15000.0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 2, "SAV000002", 25000.0, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 3, "SAV000003", 18000.0, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 4, "SAV000004", 30000.0, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 5, "SAV000005", 20000.0, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 6, "SAV000006", 35000.0, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 7, "SAV000007", 27000.0, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 8, "SAV000008", 40000.0, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 9, "SAV000009", 22000.0, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null },
                    { 10, "SAV000010", 28000.0, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Benefices",
                columns: new[] { "Id", "BenefitedSavingAccountId", "OwnerId" },
                values: new object[] { 1, 2, "f294u-ewrdm-woj93-hj3dn-8937w" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Benefices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
