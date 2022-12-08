using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class OTHourTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OTHour",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDatas",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OTHour",
                schema: "payroll",
                table: "EmployeeSalaryProcessedDatas",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
