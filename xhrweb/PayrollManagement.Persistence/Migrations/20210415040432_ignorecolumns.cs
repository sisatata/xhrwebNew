using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class ignorecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicOrGross",
                schema: "payroll",
                table: "BonusConfigurations");

            migrationBuilder.DropColumn(
                name: "PartialOn",
                schema: "payroll",
                table: "BonusConfigurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasicOrGross",
                schema: "payroll",
                table: "BonusConfigurations",
                type: "integer",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartialOn",
                schema: "payroll",
                table: "BonusConfigurations",
                type: "integer",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }
    }
}
