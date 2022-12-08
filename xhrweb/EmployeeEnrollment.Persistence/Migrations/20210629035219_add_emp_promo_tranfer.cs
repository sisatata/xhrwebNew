using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class add_emp_promo_tranfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePromotionTransfers",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    PreviousCompanyId = table.Column<Guid>(nullable: true),
                    PreviousBranchId = table.Column<Guid>(nullable: true),
                    PreviousDepartmentId = table.Column<Guid>(nullable: true),
                    PreviousPositionId = table.Column<Guid>(nullable: true),
                    NewCompanyId = table.Column<Guid>(nullable: true),
                    NewBranchId = table.Column<Guid>(nullable: true),
                    NewDepartmentId = table.Column<Guid>(nullable: true),
                    NewPositionId = table.Column<Guid>(nullable: true),
                    ProposedDate = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PreviousGross = table.Column<decimal>(nullable: true),
                    NewGross = table.Column<decimal>(nullable: true),
                    PreviousBasic = table.Column<decimal>(nullable: true),
                    NewBasic = table.Column<decimal>(nullable: true),
                    PreviousHouserent = table.Column<decimal>(nullable: true),
                    NewHouserent = table.Column<decimal>(nullable: true),
                    IncrementTypeId = table.Column<int>(nullable: true),
                    IncrementValue = table.Column<decimal>(nullable: true),
                    IncrementAmount = table.Column<decimal>(nullable: true),
                    IncrementOn = table.Column<int>(nullable: true),
                    Reason = table.Column<string>(maxLength: 500, nullable: true),
                    Reference = table.Column<string>(maxLength: 250, nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    ApproveBy = table.Column<Guid>(nullable: true),
                    ApproveNote = table.Column<string>(maxLength: 250, nullable: true),
                    ApprovalStatus = table.Column<string>(maxLength: 3, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePromotionTransfers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePromotionTransfers",
                schema: "employee");
        }
    }
}
