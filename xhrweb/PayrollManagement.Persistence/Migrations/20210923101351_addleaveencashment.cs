using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addleaveencashment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeLeaveEncashments",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    LeaveTypeId = table.Column<Guid>(nullable: true),
                    EncashDate = table.Column<DateTime>(maxLength: 10, nullable: false),
                    FinancialYearId = table.Column<Guid>(nullable: true),
                    MonthCycleId = table.Column<Guid>(nullable: true),
                    ELEncashedDays = table.Column<decimal>(nullable: true),
                    EncashedAmount = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveEncashments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeaveEncashments",
                schema: "payroll");
        }
    }
}
