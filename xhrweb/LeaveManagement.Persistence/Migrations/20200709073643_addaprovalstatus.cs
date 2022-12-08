using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class addaprovalstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                schema: "leave",
                table: "LeaveApplications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                schema: "leave",
                table: "LeaveApplications");
        }
    }
}
