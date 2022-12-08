using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class addholiday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Holidays",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HolidayDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(maxLength: 100, nullable: true),
                    ReasonLocalized = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays",
                schema: "attendance");
        }
    }
}
