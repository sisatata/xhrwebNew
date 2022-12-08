using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    BranchName = table.Column<string>(maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    BranchLocalizedName = table.Column<string>(maxLength: 250, nullable: true),
                    SortOrder = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyLocalizedName = table.Column<string>(maxLength: 250, nullable: true),
                    CompanySlogan = table.Column<string>(maxLength: 150, nullable: true),
                    LicenseKey = table.Column<string>(maxLength: 150, nullable: true),
                    SortOrder = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentCompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    DepartmentLocalizedName = table.Column<string>(maxLength: 250, nullable: true),
                    SortOrder = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LinkedEntityId = table.Column<Guid>(nullable: false),
                    LinkedEntityType = table.Column<string>(maxLength: 50, nullable: false),
                    DesignationName = table.Column<string>(maxLength: 250, nullable: false),
                    DesignationLocalizedName = table.Column<string>(maxLength: 250, nullable: true),
                    ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    SortOrder = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branch",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Designations",
                schema: "main");
        }
    }
}
