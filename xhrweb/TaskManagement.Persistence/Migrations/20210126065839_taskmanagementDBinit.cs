using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagement.Persistence.Migrations
{
    public partial class taskmanagementDBinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "task");

            migrationBuilder.CreateTable(
                name: "TaskCategories",
                schema: "task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TaskCategoryName = table.Column<string>(maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetailLogs",
                schema: "task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TaskDetailId = table.Column<Guid>(nullable: true),
                    UpdateInfo = table.Column<string>(maxLength: 1024, nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    SpendTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                schema: "task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TaskTypeId = table.Column<Guid>(nullable: true),
                    TaskName = table.Column<string>(maxLength: 250, nullable: true),
                    TaskDescription = table.Column<string>(maxLength: 1024, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    ParentTaskId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskCategories",
                schema: "task");

            migrationBuilder.DropTable(
                name: "TaskDetailLogs",
                schema: "task");

            migrationBuilder.DropTable(
                name: "TaskDetails",
                schema: "task");
        }
    }
}
