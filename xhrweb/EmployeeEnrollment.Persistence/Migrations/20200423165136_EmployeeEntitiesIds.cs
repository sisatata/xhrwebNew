using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class EmployeeEntitiesIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeStatusHistoryId",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.DropColumn(
                name: "EmployeeStatusId",
                schema: "employee",
                table: "EmployeeStatuses");

            migrationBuilder.DropColumn(
                name: "EmployeePhoneId",
                schema: "employee",
                table: "EmployeePhones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeStatusHistoryId",
                schema: "employee",
                table: "EmployeeStatusHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeStatusId",
                schema: "employee",
                table: "EmployeeStatuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeePhoneId",
                schema: "employee",
                table: "EmployeePhones",
                type: "uuid",
                nullable: true);
        }
    }
}
