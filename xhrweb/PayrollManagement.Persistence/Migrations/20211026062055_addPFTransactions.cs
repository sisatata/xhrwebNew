using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addPFTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePFTransactions",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    EmlpoyeeDesignationId = table.Column<Guid>(nullable: true),
                    EmployeeDepartmentId = table.Column<Guid>(nullable: true),
                    PFYearId = table.Column<Guid>(nullable: true),
                    PFMonthId = table.Column<Guid>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    CompanyContribution = table.Column<decimal>(nullable: true),
                    EmployeeContribution = table.Column<decimal>(nullable: true),
                    EmployeeInterestRate = table.Column<decimal>(nullable: true),
                    CompanyInterestRate = table.Column<decimal>(nullable: true),
                    InterestOnEmployeeContribution = table.Column<decimal>(nullable: true),
                    InterestOnCompanyContribution = table.Column<decimal>(nullable: true),
                    TotalContribution = table.Column<decimal>(nullable: true),
                    TotalInterest = table.Column<decimal>(nullable: true),
                    EmployeeCumulativeBalance = table.Column<decimal>(nullable: true),
                    CompanyCumulativeBalance = table.Column<decimal>(nullable: true),
                    TotalCumulativeBalance = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePFTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePFTransactions",
                schema: "payroll");
        }
    }
}
