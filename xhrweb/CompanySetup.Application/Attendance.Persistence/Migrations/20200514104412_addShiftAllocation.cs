using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class addShiftAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftAllocations",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: false),
                    DutyYear = table.Column<int>(nullable: false),
                    MonthCycleId = table.Column<Guid>(nullable: false),
                    DutyMonth = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    PrimaryShiftId = table.Column<Guid>(nullable: false),
                    RotationDay = table.Column<Guid>(nullable: false),
                    C1 = table.Column<string>(nullable: true),
                    C2 = table.Column<string>(nullable: true),
                    C3 = table.Column<string>(nullable: true),
                    C4 = table.Column<string>(nullable: true),
                    C5 = table.Column<string>(nullable: true),
                    C6 = table.Column<string>(nullable: true),
                    C7 = table.Column<string>(nullable: true),
                    C8 = table.Column<string>(nullable: true),
                    C9 = table.Column<string>(nullable: true),
                    C10 = table.Column<string>(nullable: true),
                    C11 = table.Column<string>(nullable: true),
                    C12 = table.Column<string>(nullable: true),
                    C13 = table.Column<string>(nullable: true),
                    C14 = table.Column<string>(nullable: true),
                    C15 = table.Column<string>(nullable: true),
                    C16 = table.Column<string>(nullable: true),
                    C17 = table.Column<string>(nullable: true),
                    C18 = table.Column<string>(nullable: true),
                    C19 = table.Column<string>(nullable: true),
                    C20 = table.Column<string>(nullable: true),
                    C21 = table.Column<string>(nullable: true),
                    C22 = table.Column<string>(nullable: true),
                    C23 = table.Column<string>(nullable: true),
                    C24 = table.Column<string>(nullable: true),
                    C25 = table.Column<string>(nullable: true),
                    C26 = table.Column<string>(nullable: true),
                    C27 = table.Column<string>(nullable: true),
                    C28 = table.Column<string>(nullable: true),
                    C29 = table.Column<string>(nullable: true),
                    C30 = table.Column<string>(nullable: true),
                    C31 = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAllocations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftAllocations",
                schema: "attendance");
        }
    }
}
