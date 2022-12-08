using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addemployeeidbankaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentOptionId",
                schema: "payroll",
                table: "EmployeeBankAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "payroll",
                table: "EmployeeBankAccounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "payroll",
                table: "EmployeeBankAccounts");

            migrationBuilder.AddColumn<short>(
                name: "PaymentOptionId",
                schema: "payroll",
                table: "EmployeeBankAccounts",
                type: "smallint",
                nullable: true);
        }
    }
}
