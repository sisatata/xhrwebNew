using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class columnaddedleavapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressOnLeave",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HalfDayLeaveTypeId",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHalfDayLeave",
                schema: "leave",
                table: "LeaveApplications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressOnLeave",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "HalfDayLeaveTypeId",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "IsHalfDayLeave",
                schema: "leave",
                table: "LeaveApplications");
        }
    }
}
