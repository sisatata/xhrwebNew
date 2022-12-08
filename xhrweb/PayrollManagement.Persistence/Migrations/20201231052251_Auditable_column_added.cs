using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class Auditable_column_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxPayerTypes",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxPayerTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxPayerTypes",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxPayerTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxParameters",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxParameters",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxInvestments",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxInvestments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxInvestments",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxInvestments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeePaidIncomeTaxes",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    FinancialYear = table.Column<string>(maxLength: 20, nullable: true),
                    MonthName = table.Column<string>(maxLength: 20, nullable: true),
                    FinancialYearId = table.Column<Guid>(nullable: true),
                    MonthCycleId = table.Column<Guid>(nullable: true),
                    Basic = table.Column<decimal>(nullable: true),
                    HouseRent = table.Column<decimal>(nullable: true),
                    Medical = table.Column<decimal>(nullable: true),
                    Conveyance = table.Column<decimal>(nullable: true),
                    FestivalBonus = table.Column<decimal>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: true),
                    ProcessingDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaidIncomeTaxes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePaidIncomeTaxes",
                schema: "payroll");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxSlabs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxSlabs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxSlabs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxSlabs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxPayerTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxPayerTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxPayerTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxPayerTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxParameters");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxParameters");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxParameters");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxParameters");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "IncomeTaxInvestments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "IncomeTaxInvestments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "IncomeTaxInvestments");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "IncomeTaxInvestments");
        }
    }
}
