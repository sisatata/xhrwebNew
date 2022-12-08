using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class companymaskidadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyMaskingId",
                schema: "main",
                table: "Company",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                schema: "main",
                table: "CommonLookUpTypes",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyMaskingId",
                schema: "main",
                table: "Company");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                schema: "main",
                table: "CommonLookUpTypes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
