using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBank.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Data_in_seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 1, "f294u-ewrdm-woj93-hj3dn-8937w" });

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 1, "f294u-ewrdm-woj93-hj3dn-8937w" });

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 1, "n8d7x-qsj3p-lf9b2-hu6zr-4579y" });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Principal", "Status", "UserId" },
                values: new object[] { true, 1, "f294u-ewrdm-woj93-hj3dn-8937w" });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Principal", "Status", "UserId" },
                values: new object[] { true, 1, "n8d7x-qsj3p-lf9b2-hu6zr-4579y" });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 1, "n8d7x-qsj3p-lf9b2-hu6zr-4579y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Principal", "Status", "UserId" },
                values: new object[] { false, 0, null });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Principal", "Status", "UserId" },
                values: new object[] { false, 0, null });

            migrationBuilder.UpdateData(
                table: "SavingAccounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Status", "UserId" },
                values: new object[] { 0, null });
        }
    }
}
