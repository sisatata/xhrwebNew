using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attenreqtypedatatypechange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestTypeId",
                schema: "attendance",
                table: "AttendanceRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestTypeId",
                schema: "attendance",
                table: "AttendanceRequests",
                type: "uuid",
                nullable: true);
        }
    }
}
