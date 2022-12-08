using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class AddAttachmentBillClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillAttachmentName",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillAttachmentName",
                schema: "payroll",
                table: "BenefitBillClaims");
        }
    }
}
