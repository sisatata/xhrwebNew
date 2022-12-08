using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class addMonthCycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthCycles",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: false),
                    MonthCycleName = table.Column<string>(maxLength: 50, nullable: false),
                    MonthCycleLocalizedName = table.Column<string>(maxLength: 100, nullable: true),
                    MonthStartDate = table.Column<DateTime>(nullable: false),
                    MonthEndDate = table.Column<DateTime>(nullable: false),
                    TotalWorkingDays = table.Column<double>(nullable: false),
                    RunningFlag = table.Column<bool>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthCycles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthCycles",
                schema: "main");
        }
    }
}
