using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanySetup.Persistence.Migrations
{
    public partial class branchmodelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SortOrder",
                schema: "main",
                table: "Branch",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "SortOrder",
                schema: "main",
                table: "Branch",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
