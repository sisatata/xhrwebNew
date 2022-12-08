using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class renamecompanyentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPhone",
                schema: "main",
                table: "CompanyPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmail",
                schema: "main",
                table: "CompanyEmail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddress",
                schema: "main",
                table: "CompanyAddress");

            migrationBuilder.RenameTable(
                name: "CompanyPhone",
                schema: "main",
                newName: "CompanyPhones",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CompanyEmail",
                schema: "main",
                newName: "CompanyEmails",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CompanyAddress",
                schema: "main",
                newName: "CompanyAddresses",
                newSchema: "main");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPhones",
                schema: "main",
                table: "CompanyPhones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmails",
                schema: "main",
                table: "CompanyEmails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddresses",
                schema: "main",
                table: "CompanyAddresses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPhones",
                schema: "main",
                table: "CompanyPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmails",
                schema: "main",
                table: "CompanyEmails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddresses",
                schema: "main",
                table: "CompanyAddresses");

            migrationBuilder.RenameTable(
                name: "CompanyPhones",
                schema: "main",
                newName: "CompanyPhone",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CompanyEmails",
                schema: "main",
                newName: "CompanyEmail",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CompanyAddresses",
                schema: "main",
                newName: "CompanyAddress",
                newSchema: "main");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPhone",
                schema: "main",
                table: "CompanyPhone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmail",
                schema: "main",
                table: "CompanyEmail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddress",
                schema: "main",
                table: "CompanyAddress",
                column: "Id");
        }
    }
}
