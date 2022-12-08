using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class StatusIdTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatusHistories",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "StatusId",
                schema: "employee",
                table: "Employees",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "employee",
                table: "Employees");
        }
    }
}
