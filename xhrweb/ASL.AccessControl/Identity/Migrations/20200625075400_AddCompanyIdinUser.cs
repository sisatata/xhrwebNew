using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASL.AccessControl.Identity.Migrations
{
    public partial class AddCompanyIdinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "access",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "access",
                table: "AspNetUsers");
        }
    }
}
