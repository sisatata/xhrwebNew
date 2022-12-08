using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class addleavetypegroupidinleavetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeGroupId",
                schema: "leave",
                table: "LeaveTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveTypeGroupId",
                schema: "leave",
                table: "LeaveTypes");
        }
    }
}
