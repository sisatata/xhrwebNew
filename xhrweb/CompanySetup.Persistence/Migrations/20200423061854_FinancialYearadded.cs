using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class FinancialYearadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "SortOrder",
                schema: "main",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    FinancialYearName = table.Column<string>(maxLength: 50, nullable: false),
                    FinancialYearLocalizedName = table.Column<string>(maxLength: 100, nullable: true),
                    FinancialYearStartDate = table.Column<DateTime>(nullable: false),
                    FinancialYearEndDate = table.Column<DateTime>(nullable: false),
                    IsRunningYear = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<short>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialYears",
                schema: "main");

            migrationBuilder.AlterColumn<long>(
                name: "SortOrder",
                schema: "main",
                table: "Branch",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
