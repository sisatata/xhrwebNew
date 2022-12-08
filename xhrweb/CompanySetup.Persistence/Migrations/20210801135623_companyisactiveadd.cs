using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class companyisactiveadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "main",
                table: "Company",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "main",
                table: "Company");
        }
    }
}
