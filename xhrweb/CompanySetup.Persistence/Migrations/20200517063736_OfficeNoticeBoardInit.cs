using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class OfficeNoticeBoardInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfficeNotices",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 250, nullable: true),
                    Details = table.Column<string>(maxLength: 2000, nullable: true),
                    IsGeneral = table.Column<bool>(nullable: false),
                    IsSectionSpecific = table.Column<bool>(nullable: false),
                    ApplicableSections = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeNotices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfficeNotices",
                schema: "main");
        }
    }
}
