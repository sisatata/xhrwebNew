using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class notificationentitymodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPushed",
                schema: "main",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NotificationOwnerAccessToken",
                schema: "main",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PushedTime",
                schema: "main",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPushed",
                schema: "main",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationOwnerAccessToken",
                schema: "main",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PushedTime",
                schema: "main",
                table: "Notifications");
        }
    }
}
