using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class addauditablecolumnnotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "main",
                table: "Notifications",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "main",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "main",
                table: "Notifications",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "main",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActivityLogTypeId = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    Comment = table.Column<string>(maxLength: 4000, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 20, nullable: true),
                    EntityId = table.Column<Guid>(nullable: true),
                    EntityName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogTypes",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SystemKeyword = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ActivityLogTypes",
                schema: "main");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "main",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "main",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "main",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "main",
                table: "Notifications");
        }
    }
}
