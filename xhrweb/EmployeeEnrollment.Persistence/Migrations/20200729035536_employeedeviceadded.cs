using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class employeedeviceadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDevices",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccessToken = table.Column<string>(maxLength: 250, nullable: true),
                    Location = table.Column<string>(maxLength: 150, nullable: true),
                    Device = table.Column<string>(maxLength: 250, nullable: true),
                    OperatingSystem = table.Column<string>(maxLength: 100, nullable: true),
                    OSVersion = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDevices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDevices",
                schema: "employee");
        }
    }
}
