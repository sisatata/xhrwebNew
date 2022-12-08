using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class add4columnspreptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationRuleName",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentOptionName",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryStructureName",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationRuleName",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.DropColumn(
                name: "PaymentOptionName",
                schema: "employee",
                table: "EmployeeRawDataPreps");

            migrationBuilder.DropColumn(
                name: "SalaryStructureName",
                schema: "employee",
                table: "EmployeeRawDataPreps");
        }
    }
}
