using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class fileidaddedinprep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "employee",
                table: "RawFileDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "employee",
                table: "RawFileDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "employee",
                table: "RawFileDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "employee",
                table: "RawFileDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "employee",
                table: "RawFileDetails",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalFail",
                schema: "employee",
                table: "RawFileDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRecord",
                schema: "employee",
                table: "RawFileDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSuccess",
                schema: "employee",
                table: "RawFileDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RawFileDetailId",
                schema: "employee",
                table: "EmployeeRawDataPreps",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "TotalFail",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "TotalRecord",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "TotalSuccess",
                schema: "employee",
                table: "RawFileDetails");

            migrationBuilder.DropColumn(
                name: "RawFileDetailId",
                schema: "employee",
                table: "EmployeeRawDataPreps");
        }
    }
}
