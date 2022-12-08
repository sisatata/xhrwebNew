using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class attendancedataprocessinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceProcessedData",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    PunchDate = table.Column<DateTime>(nullable: false),
                    PunchYear = table.Column<string>(maxLength: 10, nullable: true),
                    PunchMonth = table.Column<int>(nullable: true),
                    TimeIn = table.Column<DateTime>(nullable: true),
                    TimeOut = table.Column<DateTime>(nullable: true),
                    ShiftIn = table.Column<DateTime>(nullable: true),
                    ShiftOut = table.Column<DateTime>(nullable: true),
                    BreakIn = table.Column<DateTime>(nullable: true),
                    BreakOut = table.Column<DateTime>(nullable: true),
                    BreakLate = table.Column<DateTime>(nullable: true),
                    Late = table.Column<DateTime>(nullable: true),
                    ShiftId = table.Column<Guid>(nullable: true),
                    ShiftCode = table.Column<string>(maxLength: 3, nullable: true),
                    RegularHour = table.Column<DateTime>(nullable: true),
                    OTHour = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(maxLength: 3, nullable: true),
                    Status_V2 = table.Column<string>(maxLength: 3, nullable: true),
                    BuyerShiftIn = table.Column<DateTime>(nullable: true),
                    BuyerShiftOut = table.Column<DateTime>(nullable: true),
                    BuyerOTTime = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    IsLunchOut = table.Column<bool>(nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: true),
                    MonthCycleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceProcessedData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceProcessedData",
                schema: "attendance");
        }
    }
}
