using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class AddLeaveBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveBalance",
                schema: "leave",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    LeaveTypeId = table.Column<Guid>(nullable: false),
                    LeaveCalendar = table.Column<string>(maxLength: 20, nullable: false),
                    MaximumBalance = table.Column<double>(nullable: false),
                    UsedBalance = table.Column<double>(nullable: false),
                    RemainingBalance = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalance", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveBalance",
                schema: "leave");
        }
    }
}
