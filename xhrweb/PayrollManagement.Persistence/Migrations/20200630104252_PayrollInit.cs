using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class PayrollInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payroll");

            migrationBuilder.CreateTable(
                name: "BenefitDeductionCodes",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    BenifitDeductionCode = table.Column<string>(maxLength: 50, nullable: true),
                    BenifitDeductionCodeName = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitDeductionCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BenefitDeductionConfigs",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BenefitDeductionCode = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Type = table.Column<string>(maxLength: 15, nullable: true),
                    BasicOrGross = table.Column<string>(maxLength: 15, nullable: true),
                    CalculationType = table.Column<string>(maxLength: 15, nullable: true),
                    PercentOfBasicOrGross = table.Column<decimal>(nullable: true),
                    FixedAmount = table.Column<decimal>(nullable: true),
                    IntervalId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsCalculateSalary = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitDeductionConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BenefitDeductionEmployeeAssigneds",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BenefitDeductionId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitDeductionEmployeeAssigneds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BenefitDeductionIntervals",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IntervalId = table.Column<int>(nullable: true),
                    IntervalName = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitDeductionIntervals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitDeductionCodes",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "BenefitDeductionConfigs",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "BenefitDeductionEmployeeAssigneds",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "BenefitDeductionIntervals",
                schema: "payroll");
        }
    }
}
