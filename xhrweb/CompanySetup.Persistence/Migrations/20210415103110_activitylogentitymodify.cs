using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class activitylogentitymodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "main",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "main",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "main",
                table: "ActivityLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "main",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "main",
                table: "ActivityLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "main",
                table: "ActivityLogs",
                type: "uuid",
                nullable: true);
        }
    }
}
