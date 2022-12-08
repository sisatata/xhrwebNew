using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class bonusentityadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonusConfigurations",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    ReligionId = table.Column<Guid>(nullable: true),
                    RangeFromInMonth = table.Column<int>(nullable: true),
                    RangeToInMonth = table.Column<int>(nullable: true),
                    BasicOrGrossId = table.Column<int>(nullable: false),
                    PercentOfBasicOrGross = table.Column<int>(nullable: true),
                    FixedAmount = table.Column<int>(nullable: true),
                    IsPaidFull = table.Column<bool>(nullable: false),
                    PartialOnId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartialOn = table.Column<int>(maxLength: 50, nullable: false),
                    BasicOrGross = table.Column<int>(maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBonusProcessedDatas",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    BonusTypeId = table.Column<Guid>(nullable: true),
                    BonusDate = table.Column<DateTime>(nullable: true),
                    FinancialYearId = table.Column<Guid>(nullable: true),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true),
                    PositionId = table.Column<Guid>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: true),
                    GradeId = table.Column<Guid>(nullable: true),
                    ReligionId = table.Column<Guid>(nullable: true),
                    Basic = table.Column<decimal>(nullable: true),
                    HouseRent = table.Column<decimal>(nullable: true),
                    Medical = table.Column<decimal>(nullable: true),
                    Conveyance = table.Column<decimal>(nullable: true),
                    Food = table.Column<decimal>(nullable: true),
                    GrossSalary = table.Column<decimal>(nullable: true),
                    PayableBonus = table.Column<decimal>(nullable: true),
                    TaxDeductedAmount = table.Column<decimal>(nullable: true),
                    NetPayableBonus = table.Column<decimal>(nullable: true),
                    Basic_V2 = table.Column<decimal>(nullable: true),
                    HouseRent_V2 = table.Column<decimal>(nullable: true),
                    GrossSalary_V2 = table.Column<decimal>(nullable: true),
                    PayableBonus_V2 = table.Column<decimal>(nullable: true),
                    TaxDeductedAmount_V2 = table.Column<decimal>(nullable: true),
                    NetPayableBonus_V2 = table.Column<decimal>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApprovedBy = table.Column<Guid>(nullable: true),
                    ApprovedTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BonusConfigurationId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBonusProcessedDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusConfigurations",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeBonusProcessedDatas",
                schema: "payroll");
        }
    }
}
