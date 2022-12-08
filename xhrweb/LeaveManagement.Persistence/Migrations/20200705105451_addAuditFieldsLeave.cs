using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class addAuditFieldsLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveTypes",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveTypes",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveBalance",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveBalance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveBalance",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveBalance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveApplications",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveApplications",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveBalance");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveBalance");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveBalance");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveBalance");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "leave",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "leave",
                table: "LeaveApplications");
        }
    }
}
