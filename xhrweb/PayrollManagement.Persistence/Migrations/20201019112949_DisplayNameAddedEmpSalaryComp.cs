using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class DisplayNameAddedEmpSalaryComp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDataComponents",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDataComponents",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDataComponents");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDataComponents");
        }
    }
}
