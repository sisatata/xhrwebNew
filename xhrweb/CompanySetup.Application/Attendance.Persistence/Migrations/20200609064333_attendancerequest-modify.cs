using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendancerequestmodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "attendance",
                table: "AttendanceRequests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "attendance",
                table: "AttendanceRequests");
        }
    }
}
