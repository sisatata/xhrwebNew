using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class EmployeeFamilyManagerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFamilyMembers",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    FamiliyMemberName = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    EducationClass = table.Column<string>(maxLength: 150, nullable: true),
                    EducationalInstitute = table.Column<string>(maxLength: 150, nullable: true),
                    EducationYear = table.Column<string>(maxLength: 20, nullable: true),
                    RelationTypeId = table.Column<Guid>(nullable: false),
                    IsDependant = table.Column<bool>(nullable: false),
                    IsEligibleForMedical = table.Column<bool>(nullable: false),
                    IsEligibleForEducation = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFamilyMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeManagers",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: true),
                    IsPrimaryManager = table.Column<bool>(nullable: false),
                    AssignedBy = table.Column<Guid>(nullable: true),
                    AssignDate = table.Column<DateTime>(nullable: true),
                    UnAssignedBy = table.Column<Guid>(nullable: true),
                    UnAssignDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    ManagerTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeManagers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFamilyMembers",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeManagers",
                schema: "employee");
        }
    }
}
