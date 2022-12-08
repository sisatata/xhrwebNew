using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class addcompanyentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyAddress",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    AddressLine1 = table.Column<string>(maxLength: 150, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 150, nullable: true),
                    Village = table.Column<string>(maxLength: 50, nullable: true),
                    PostOffice = table.Column<string>(maxLength: 50, nullable: true),
                    ThanaId = table.Column<short>(nullable: true),
                    DistrictId = table.Column<short>(nullable: true),
                    CountryId = table.Column<short>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    AddressTypeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmail",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(maxLength: 150, nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPhone",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PhoneTypeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPhone", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAddress",
                schema: "main");

            migrationBuilder.DropTable(
                name: "CompanyEmail",
                schema: "main");

            migrationBuilder.DropTable(
                name: "CompanyPhone",
                schema: "main");
        }
    }
}
