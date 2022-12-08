using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class AddLeaveApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                schema: "leave",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    LeaveTypeId = table.Column<Guid>(nullable: false),
                    LeaveCalendar = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NoOfDays = table.Column<double>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    LeaveApplicationStatus = table.Column<string>(maxLength: 50, nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ApplyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveApplications",
                schema: "leave");
        }
    }
}
