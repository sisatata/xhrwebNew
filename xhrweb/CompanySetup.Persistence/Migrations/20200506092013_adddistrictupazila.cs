using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class adddistrictupazila : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DivisionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    NameLocalized = table.Column<string>(maxLength: 50, nullable: true),
                    Latitude = table.Column<float>(nullable: true),
                    Longitude = table.Column<float>(nullable: true),
                    Website = table.Column<string>(maxLength: 150, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upazilas",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DistrictId = table.Column<Guid>(nullable: false),
                    UpazilaName = table.Column<string>(maxLength: 50, nullable: true),
                    UpazilaNameLocalized = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upazilas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Districts",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Upazilas",
                schema: "main");
        }
    }
}
