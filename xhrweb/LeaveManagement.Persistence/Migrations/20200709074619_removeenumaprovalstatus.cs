using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class removeenumaprovalstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveApplicationStatus",
                schema: "leave",
                table: "LeaveApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveApplicationStatus",
                schema: "leave",
                table: "LeaveApplications",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
