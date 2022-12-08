using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Persistence.Migrations
{
    public partial class change_TaskDetail_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "task",
                table: "TaskDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "task",
                table: "TaskDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "task",
                table: "TaskDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "task",
                table: "TaskDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "task",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "task",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "task",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "task",
                table: "TaskDetails");
        }
    }
}
