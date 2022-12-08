using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addAuditFieldsall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeStatusHistories",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeStatusHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeStatusHistories",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeStatusHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeManagers",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeManagers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeManagers",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeManagers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeEnrollments",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeEnrollments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeEnrollments",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeEnrollments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeCards",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeCards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeCards",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeManagers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeManagers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeManagers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeManagers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeEnrollments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeEnrollments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeEnrollments");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeEnrollments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "EmployeeCards");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "EmployeeCards");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "EmployeeCards");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "EmployeeCards");
        }
    }
}
