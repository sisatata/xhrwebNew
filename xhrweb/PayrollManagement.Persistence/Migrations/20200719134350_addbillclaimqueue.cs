using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addbillclaimqueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenefitBillClaimApproveQueues",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BenefitBillClaimId = table.Column<Guid>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    ApprovedAmount = table.Column<decimal>(nullable: true),
                    ApprovalStatus = table.Column<string>(maxLength: 3, nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitBillClaimApproveQueues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitBillClaimApproveQueues",
                schema: "payroll");
        }
    }
}
