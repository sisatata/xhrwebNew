using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addincometax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeTaxInvestments",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvestmentPercentage = table.Column<decimal>(nullable: true),
                    WaiverPercentage = table.Column<decimal>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxInvestments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxParameters",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParameterName = table.Column<string>(nullable: true),
                    LimitAmount = table.Column<decimal>(nullable: true),
                    LimitPercentageOfBasic = table.Column<decimal>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    PayerTypeCode = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxPayerTypes",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PayerTypeCode = table.Column<string>(maxLength: 20, nullable: true),
                    PayerTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxPayerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeTaxSlabs",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SlabName = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRunningFlag = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    SlabAmount = table.Column<decimal>(nullable: true),
                    TaxablePercent = table.Column<decimal>(nullable: true),
                    IncomeTaxPayerTypeId = table.Column<Guid>(nullable: true),
                    SlabOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxSlabs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeTaxInvestments",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "IncomeTaxParameters",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "IncomeTaxPayerTypes",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "IncomeTaxSlabs",
                schema: "payroll");
        }
    }
}
