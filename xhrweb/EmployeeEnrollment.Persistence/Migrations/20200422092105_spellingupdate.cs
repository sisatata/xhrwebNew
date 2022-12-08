using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class spellingupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FulNameLC",
                schema: "employee",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FullNameLC",
                schema: "employee",
                table: "Employees",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullNameLC",
                schema: "employee",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FulNameLC",
                schema: "employee",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
