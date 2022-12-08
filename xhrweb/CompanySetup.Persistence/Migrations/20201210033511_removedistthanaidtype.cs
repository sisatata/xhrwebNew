using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class removedistthanaidtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "main",
                table: "CompanyAddresses");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "main",
                table: "CompanyAddresses");

            migrationBuilder.DropColumn(
                name: "ThanaId",
                schema: "main",
                table: "CompanyAddresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CountryId",
                schema: "main",
                table: "CompanyAddresses",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "DistrictId",
                schema: "main",
                table: "CompanyAddresses",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ThanaId",
                schema: "main",
                table: "CompanyAddresses",
                type: "smallint",
                nullable: true);
        }
    }
}
