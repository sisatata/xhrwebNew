using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addstatusinttype2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatusHistories");

            migrationBuilder.AddColumn<short>(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatuses",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatuses");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                schema: "employee",
                table: "EmployeeStatusHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
