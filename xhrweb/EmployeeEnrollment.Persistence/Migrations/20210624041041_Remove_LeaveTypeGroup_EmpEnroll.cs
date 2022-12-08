using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class Remove_LeaveTypeGroup_EmpEnroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveTypeGroup",
                schema: "employee",
                table: "EmployeeEnrollments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveTypeGroup",
                schema: "employee",
                table: "EmployeeEnrollments",
                type: "text",
                nullable: true);
        }
    }
}
