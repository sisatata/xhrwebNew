using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class CommonLookupTypeInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommonLookUpTypes",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShortCode = table.Column<string>(maxLength: 20, nullable: true),
                    LookUpTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    LookUpTypeNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    SortOrder = table.Column<short>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLookUpTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonLookUpTypes",
                schema: "main");
        }
    }
}
