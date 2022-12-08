using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class EmployeeExperienceAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeExperiences",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 150, nullable: true),
                    Designation = table.Column<string>(maxLength: 150, nullable: true),
                    FunctionalDesignation = table.Column<string>(maxLength: 150, nullable: true),
                    Department = table.Column<string>(maxLength: 150, nullable: true),
                    Responsibilities = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyAddress = table.Column<string>(maxLength: 150, nullable: true),
                    CompanyContactNo = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyMobile = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyEmail = table.Column<string>(maxLength: 150, nullable: true),
                    CompanyWeb = table.Column<string>(maxLength: 50, nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: true),
                    TilDate = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExperiences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeExperiences",
                schema: "employee");
        }
    }
}
