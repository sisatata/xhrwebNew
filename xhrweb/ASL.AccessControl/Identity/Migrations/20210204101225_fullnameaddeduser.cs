using Microsoft.EntityFrameworkCore.Migrations;

namespace ASL.AccessControl.Identity.Migrations
{
    public partial class fullnameaddeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "access",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "access",
                table: "AspNetUsers");
        }
    }
}
