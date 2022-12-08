using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class removepayertypecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeTaxPayerTypeId",
                schema: "payroll",
                table: "IncomeTaxSlabs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IncomeTaxPayerTypeId",
                schema: "payroll",
                table: "IncomeTaxSlabs",
                type: "uuid",
                nullable: true);
        }
    }
}
