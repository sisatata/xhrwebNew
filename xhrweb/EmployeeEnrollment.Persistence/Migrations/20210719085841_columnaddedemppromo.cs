using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class columnaddedemppromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewGradeId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "NewPaymentOptionId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewSalaryStructureId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousGradeId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PreviousPaymentOptionId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousSalaryStructureId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewGradeId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "NewPaymentOptionId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "NewSalaryStructureId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "PreviousGradeId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "PreviousPaymentOptionId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "PreviousSalaryStructureId",
                schema: "employee",
                table: "EmployeePromotionTransfers");
        }
    }
}
