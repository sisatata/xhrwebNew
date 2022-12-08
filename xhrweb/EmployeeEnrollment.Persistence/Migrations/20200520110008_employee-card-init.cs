using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class employeecardinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCards",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    CardMaskingValue = table.Column<string>(maxLength: 20, nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    ChargeAmount = table.Column<decimal>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    CardRevoked = table.Column<bool>(nullable: false),
                    CardRevokedDate = table.Column<DateTime>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCards",
                schema: "employee");
        }
    }
}
