using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addAuditFieldsPayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitDeductionConfigs",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitDeductionConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitDeductionConfigs",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitDeductionConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitDeductionEmployeeAssigneds");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitDeductionConfigs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitDeductionConfigs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitDeductionConfigs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitDeductionConfigs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "payroll",
                table: "BenefitBillClaims");
        }
    }
}
