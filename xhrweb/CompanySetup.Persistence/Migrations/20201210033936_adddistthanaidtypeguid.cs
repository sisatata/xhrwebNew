using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class adddistthanaidtypeguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "main",
                table: "CompanyAddresses",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                schema: "main",
                table: "CompanyAddresses",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ThanaId",
                schema: "main",
                table: "CompanyAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
