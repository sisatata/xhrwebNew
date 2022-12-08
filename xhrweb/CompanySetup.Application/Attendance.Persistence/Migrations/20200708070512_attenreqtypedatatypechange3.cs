using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attenreqtypedatatypechange3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestTypeIdd",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestTypeId",
                schema: "attendance",
                table: "AttendanceRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestTypeId",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestTypeIdd",
                schema: "attendance",
                table: "AttendanceRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
