using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class addAuditFieldsAtten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Shifts",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Shifts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Shifts",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Shifts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "ShiftGroups",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "ShiftGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "ShiftGroups",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "ShiftGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "ShiftAllocations",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "ShiftAllocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "ShiftAllocations",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "ShiftAllocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "AttendanceRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "AttendanceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "AttendanceRequests",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "AttendanceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance9",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance9",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance9",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance9",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance8",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance8",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance7",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance7",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance7",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance7",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance6",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance6",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance6",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance6",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance5",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance5",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance5",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance5",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance4",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance4",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance3",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance3",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance3",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance3",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance2",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance2",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance12",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance12",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance12",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance12",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance11",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance11",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance11",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance11",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance10",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance10",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance10",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance10",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance1",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance1",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance1",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance1",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "ShiftAllocations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "ShiftAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "ShiftAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "ShiftAllocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "AttendanceRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "attendance",
                table: "Attendance1");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "attendance",
                table: "Attendance1");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "attendance",
                table: "Attendance1");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "attendance",
                table: "Attendance1");
        }
    }
}
