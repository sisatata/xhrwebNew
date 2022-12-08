using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addbillnomaskingvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillNo",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "BillNoMaskingValue",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillNo",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "BillNoMaskingValue",
                schema: "payroll",
                table: "BenefitBillClaims");
        }
    }
}
