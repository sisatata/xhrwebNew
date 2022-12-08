using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LeaveManagement.Persistence.Migrations
{
    public partial class addleavetypegroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveTypeGroups",
                schema: "leave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeaveTypeGroupName = table.Column<string>(maxLength: 50, nullable: true),
                    LeaveTypeGroupNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeGroups", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveTypeGroups",
                schema: "leave");
        }
    }
}
