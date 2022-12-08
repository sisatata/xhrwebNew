using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class added_LeaveTypeGroup_column_in_EmployeeEnrollemnt_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveTypeGroup",
                schema: "employee",
                table: "EmployeeEnrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveTypeGroup",
                schema: "employee",
                table: "EmployeeEnrollments");
        }
    }
}
