using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addincreamentontypeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncrementOnId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncrementValueTypeId",
                schema: "employee",
                table: "EmployeePromotionTransfers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeNotes",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(maxLength: 2048, nullable: true),
                    DownloadId = table.Column<Guid>(nullable: true),
                    DisplayToEmployee = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeNotes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeNotes",
                schema: "employee");

            migrationBuilder.DropColumn(
                name: "IncrementOnId",
                schema: "employee",
                table: "EmployeePromotionTransfers");

            migrationBuilder.DropColumn(
                name: "IncrementValueTypeId",
                schema: "employee",
                table: "EmployeePromotionTransfers");
        }
    }
}
