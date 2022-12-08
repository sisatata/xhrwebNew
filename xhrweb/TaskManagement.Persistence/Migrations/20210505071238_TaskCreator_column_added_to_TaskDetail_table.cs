using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Persistence.Migrations
{
    public partial class TaskCreator_column_added_to_TaskDetail_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskCreator",
                schema: "task",
                table: "TaskDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskCreator",
                schema: "task",
                table: "TaskDetails");
        }
    }
}
