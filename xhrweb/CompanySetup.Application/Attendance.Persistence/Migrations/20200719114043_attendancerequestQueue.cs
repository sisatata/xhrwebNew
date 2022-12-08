using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendancerequestQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceRequestApproveQueues",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttendanceApplicationId = table.Column<Guid>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    ApprovalStatus = table.Column<string>(maxLength: 3, nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRequestApproveQueues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRequestApproveQueues",
                schema: "attendance");
        }
    }
}
