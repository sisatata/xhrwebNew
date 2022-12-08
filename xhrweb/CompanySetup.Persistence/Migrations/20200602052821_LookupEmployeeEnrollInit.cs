using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class LookupEmployeeEnrollInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfirmationRules",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RuleName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ConfirmationType = table.Column<short>(nullable: true),
                    ConfirmationMonths = table.Column<short>(nullable: true),
                    SortOrder = table.Column<short>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GradeName = table.Column<string>(maxLength: 50, nullable: true),
                    GradeNameLocalized = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRuleDetails",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SalaryRuleId = table.Column<Guid>(nullable: true),
                    SalaryHead = table.Column<string>(maxLength: 50, nullable: true),
                    Value = table.Column<decimal>(nullable: true),
                    ValueType = table.Column<string>(maxLength: 2, nullable: true),
                    PercentDependOn = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRuleDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRules",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RuleName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmationRules",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Grades",
                schema: "main");

            migrationBuilder.DropTable(
                name: "SalaryRuleDetails",
                schema: "main");

            migrationBuilder.DropTable(
                name: "SalaryRules",
                schema: "main");
        }
    }
}
