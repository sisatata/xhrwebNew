using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class addshiftgroupproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RotationDay",
                schema: "attendance",
                table: "ShiftGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WeekendNames",
                schema: "attendance",
                table: "ShiftGroups",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeekendTypeId",
                schema: "attendance",
                table: "ShiftGroups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RotationDay",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "WeekendNames",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "WeekendTypeId",
                schema: "attendance",
                table: "ShiftGroups");
        }
    }
}
