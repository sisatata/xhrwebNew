using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class AddNotificationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: true),
                    ApplicantId = table.Column<Guid>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    NotificationTitle = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationDetail = table.Column<string>(maxLength: 1000, nullable: true),
                    IsViewed = table.Column<bool>(nullable: false),
                    ViewedTime = table.Column<DateTime>(nullable: true),
                    NotificationTypeId = table.Column<short>(nullable: true),
                    NotificationOwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "main");
        }
    }
}
