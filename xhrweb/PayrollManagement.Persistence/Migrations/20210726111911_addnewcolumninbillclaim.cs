using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addnewcolumninbillclaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationFrom",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationTo",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfAttendees",
                schema: "payroll",
                table: "BenefitBillClaims",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAttendees",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleTypeId",
                schema: "payroll",
                table: "BenefitBillClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationFrom",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "LocationTo",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "NameOfAttendees",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "NumberOfAttendees",
                schema: "payroll",
                table: "BenefitBillClaims");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                schema: "payroll",
                table: "BenefitBillClaims");
        }
    }
}
