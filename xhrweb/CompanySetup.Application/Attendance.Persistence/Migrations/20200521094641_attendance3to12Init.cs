using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendance3to12Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendance10",
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
                    table.PrimaryKey("PK_Attendance10", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance11",
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
                    table.PrimaryKey("PK_Attendance11", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance12",
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
                    table.PrimaryKey("PK_Attendance12", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance3",
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
                    table.PrimaryKey("PK_Attendance3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance4",
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
                    table.PrimaryKey("PK_Attendance4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance5",
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
                    table.PrimaryKey("PK_Attendance5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance6",
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
                    table.PrimaryKey("PK_Attendance6", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance7",
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
                    table.PrimaryKey("PK_Attendance7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance8",
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
                    table.PrimaryKey("PK_Attendance8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance9",
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
                    table.PrimaryKey("PK_Attendance9", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance10",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance11",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance12",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance3",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance4",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance5",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance6",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance7",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance8",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "Attendance9",
                schema: "attendance");
        }
    }
}
