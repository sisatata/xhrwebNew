using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class addleaveapprovalqueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveApplicationApproveQueues",
                schema: "leave",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeaveApplicationId = table.Column<Guid>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: true),
                    ApprovalStatus = table.Column<string>(maxLength: 3, nullable: true),
                    ApprovedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplicationApproveQueues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveApplicationApproveQueues",
                schema: "leave");
        }
    }
}
