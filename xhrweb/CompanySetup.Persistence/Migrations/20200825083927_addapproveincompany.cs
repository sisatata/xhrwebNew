using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class addapproveincompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                schema: "main",
                table: "Company",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "main",
                table: "Company",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                schema: "main",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "main",
                table: "Company");
        }
    }
}
