using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class UpdateLeaveTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Balance",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ConsecutiveLimit",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EncashReserveBalance",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAdvanceLeaveApply",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowOpeningBalance",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowWithPrecedingHoliday",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBasedWorkingDays",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCarryForward",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDependOnDateOfConfirmation",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEncashable",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFemaleSpecific",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeaveDaysFixed",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreventPostLeaveApply",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ConsecutiveLimit",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "EncashReserveBalance",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsAllowAdvanceLeaveApply",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsAllowOpeningBalance",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsAllowWithPrecedingHoliday",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsBasedWorkingDays",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsCarryForward",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsDependOnDateOfConfirmation",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsEncashable",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsFemaleSpecific",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsLeaveDaysFixed",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "IsPreventPostLeaveApply",
                schema: "leave",
                table: "LeaveTypes");
        }
    }
}
