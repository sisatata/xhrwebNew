using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Persistence.Migrations
{
    public partial class TaskDetail_change_column_name_ManagerId_to_AssigneeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "task",
                table: "TaskDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                schema: "task",
                table: "TaskDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                schema: "task",
                table: "TaskDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                schema: "task",
                table: "TaskDetails",
                type: "uuid",
                nullable: true);
        }
    }
}
