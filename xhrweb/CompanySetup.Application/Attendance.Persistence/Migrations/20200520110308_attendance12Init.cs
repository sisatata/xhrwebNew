using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendance12Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendance1",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CardId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    AttendanceYear = table.Column<string>(maxLength: 10, nullable: true),
                    AttendanceDate = table.Column<DateTime>(nullable: true),
                    Punchtype = table.Column<short>(nullable: true),
                    OverNightMark = table.Column<bool>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance2",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CardId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    AttendanceYear = table.Column<string>(maxLength: 10, nullable: true),
                    AttendanceDate = table.Column<DateTime>(nullable: true),
                    Punchtype = table.Column<short>(nullable: true),
                    OverNightMark = table.Column<bool>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance2", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance1",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance2",
                schema: "attendance");
        }
    }
}
