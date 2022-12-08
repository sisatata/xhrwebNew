using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class addLatLongProcessedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TimeInLatitude",
                schema: "attendance",
                table: "AttendanceProcessedData",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TimeInLongitude",
                schema: "attendance",
                table: "AttendanceProcessedData",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TimeOutLatitude",
                schema: "attendance",
                table: "AttendanceProcessedData",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TimeOutLongitude",
                schema: "attendance",
                table: "AttendanceProcessedData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeInLatitude",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "TimeInLongitude",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "TimeOutLatitude",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "TimeOutLongitude",
                schema: "attendance",
                table: "AttendanceProcessedData");
        }
    }
}
