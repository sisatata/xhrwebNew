using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EarlyOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                schema: "attendance",
                table: "Shifts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraceIn",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GraceOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelaborShift",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRollingShift",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LunchBreakIn",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LunchBreakOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanEarlyOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanIn",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanLate",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanLunchBreakIn",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanLunchBreakOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RamadanOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Range",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegHour",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ShiftCode",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ShiftGroupID",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ShiftIn",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ShiftLate",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ShiftLocalizedName",
                schema: "attendance",
                table: "Shifts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShiftOut",
                schema: "attendance",
                table: "Shifts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                schema: "attendance",
                table: "Shifts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "EarlyOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "EndTime",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "GraceIn",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "GraceOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsRelaborShift",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsRollingShift",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "LunchBreakIn",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "LunchBreakOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanEarlyOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanIn",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanLate",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanLunchBreakIn",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanLunchBreakOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RamadanOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Range",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RegHour",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftCode",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftGroupID",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftIn",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftLate",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftLocalizedName",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftOut",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "attendance",
                table: "Shifts");
        }
    }
}
