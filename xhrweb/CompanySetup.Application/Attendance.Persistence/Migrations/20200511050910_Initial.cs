using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "attendance");

            migrationBuilder.CreateTable(
                name: "ShiftGroups",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShiftGroupName = table.Column<string>(maxLength: 150, nullable: false),
                    ShiftGroupNameLC = table.Column<string>(maxLength: 150, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShiftName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftGroups",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "attendance");
        }
    }
}
