using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class DeleteDutyYearMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyMonth",
                schema: "attendance",
                table: "ShiftAllocations");

            migrationBuilder.DropColumn(
                name: "DutyYear",
                schema: "attendance",
                table: "ShiftAllocations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DutyMonth",
                schema: "attendance",
                table: "ShiftAllocations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DutyYear",
                schema: "attendance",
                table: "ShiftAllocations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
