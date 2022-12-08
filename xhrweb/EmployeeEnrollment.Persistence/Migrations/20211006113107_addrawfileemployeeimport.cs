using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class addrawfileemployeeimport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawFileDetails",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    FileType = table.Column<string>(maxLength: 50, nullable: true),
                    Comments = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFileDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFileDetails",
                schema: "employee");
        }
    }
}
