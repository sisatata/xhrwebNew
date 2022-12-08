using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addsomenewfieldspreptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Division",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentCountry",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentCountry",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.DropColumn(
                name: "Grade",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.DropColumn(
                name: "PermanentCountry",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.DropColumn(
                name: "PresentCountry",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.AddColumn<string>(
                name: "Division",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
