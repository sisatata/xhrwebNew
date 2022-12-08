using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class EmployeeEducationInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                schema: "employee",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                schema: "employee",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderId",
                schema: "employee",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                schema: "employee",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeEducations",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    EducationalDegreeId = table.Column<Guid>(nullable: true),
                    EducationalInstituteId = table.Column<Guid>(nullable: true),
                    Session = table.Column<string>(maxLength: 20, nullable: true),
                    PassingYear = table.Column<string>(maxLength: 20, nullable: true),
                    BoardorUniversity = table.Column<string>(maxLength: 150, nullable: true),
                    Result = table.Column<string>(maxLength: 30, nullable: true),
                    ResultType = table.Column<string>(maxLength: 10, nullable: true),
                    OutOf = table.Column<decimal>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEducations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEducations",
                schema: "employee");

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                schema: "employee",
                table: "Employees",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                schema: "employee",
                table: "Employees",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "GenderId",
                schema: "employee",
                table: "Employees",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                schema: "employee",
                table: "Employees",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
