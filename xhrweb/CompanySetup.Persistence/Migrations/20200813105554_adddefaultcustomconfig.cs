using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class adddefaultcustomconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomConfigurations",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CustomValue = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultConfigurations",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    DefaultValue = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultConfigurations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomConfigurations",
                schema: "main");

            migrationBuilder.DropTable(
                name: "DefaultConfigurations",
                schema: "main");
        }
    }
}
