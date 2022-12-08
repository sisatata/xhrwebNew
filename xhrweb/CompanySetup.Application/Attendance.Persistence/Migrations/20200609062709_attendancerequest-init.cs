using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendancerequestinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceRequests",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestTypeId = table.Column<Guid>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(maxLength: 10, nullable: true),
                    AprovalStatus = table.Column<string>(maxLength: 50, nullable: true),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRequests",
                schema: "attendance");
        }
    }
}
