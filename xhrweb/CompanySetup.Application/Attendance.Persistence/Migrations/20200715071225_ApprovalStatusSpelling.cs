using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class ApprovalStatusSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprovalStatus",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                schema: "attendance",
                table: "AttendanceRequests",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.AddColumn<string>(
                name: "AprovalStatus",
                schema: "attendance",
                table: "AttendanceRequests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
