using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addbillclaimpaidstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaidBy",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SettledBy",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SettledDate",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidBy",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "SettledBy",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "SettledDate",
                schema: "payroll",
                table: "BenefitBillClaims");
        }
    }
}
