using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addpayertypecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxPayerTypeCode",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxPayerTypeCode",
                schema: "payroll",
                table: "IncomeTaxSlabs");
        }
    }
}
