using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class AddBillClaim_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenefitBillClaims",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BenefitDeductionId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    BillDate = table.Column<DateTime>(nullable: true),
                    ClaimDate = table.Column<DateTime>(nullable: true),
                    AllocatedAmount = table.Column<decimal>(nullable: true),
                    ClaimAmount = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    ApprovedAmount = table.Column<decimal>(nullable: true),
                    ApprovedBy = table.Column<Guid>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    ApprovedNotes = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    Status = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitBillClaims", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitBillClaims",
                schema: "payroll");
        }
    }
}
