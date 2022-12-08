using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "leave");

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                schema: "leave",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeaveTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    LeaveTypeShortName = table.Column<string>(maxLength: 50, nullable: true),
                    LeaveTypeLocalizedName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveTypes",
                schema: "leave");
        }
    }
}
