using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addAuditFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "Employees",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "Employees",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "Employees");
        }
    }
}
