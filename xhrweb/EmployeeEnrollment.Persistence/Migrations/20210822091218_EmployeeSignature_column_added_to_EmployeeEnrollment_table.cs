using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class EmployeeSignature_column_added_to_EmployeeEnrollment_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeSignature",
                schema: "employee",
                table: "EmployeeEnrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeSignature",
                schema: "employee",
                table: "EmployeeEnrollments");
        }
    }
}
