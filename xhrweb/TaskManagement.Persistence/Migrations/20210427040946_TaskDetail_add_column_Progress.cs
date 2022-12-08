using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Persistence.Migrations
{
    public partial class TaskDetail_add_column_Progress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Progress",
                schema: "task",
                table: "TaskDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                schema: "task",
                table: "TaskDetails");
        }
    }
}
