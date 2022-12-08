using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollManagement.Persistence.Migrations
{
    public partial class addpayrolltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankBranches",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BranchName = table.Column<string>(maxLength: 50, nullable: true),
                    BranchNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    BranchAddress = table.Column<string>(maxLength: 250, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 50, nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ContactEmailId = table.Column<string>(maxLength: 50, nullable: true),
                    SortOrder = table.Column<int>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BankName = table.Column<string>(maxLength: 50, nullable: true),
                    BankNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    SortOrder = table.Column<int>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBankAccounts",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BankId = table.Column<Guid>(nullable: true),
                    BankBranchId = table.Column<Guid>(nullable: true),
                    AccountNo = table.Column<string>(maxLength: 20, nullable: true),
                    AccountTitle = table.Column<string>(maxLength: 100, nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true),
                    PositionId = table.Column<Guid>(nullable: true),
                    GradeId = table.Column<Guid>(nullable: true),
                    SalaryStructureId = table.Column<Guid>(nullable: true),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    GrossSalary = table.Column<decimal>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryComponents",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeSalaryId = table.Column<Guid>(nullable: true),
                    SalaryStructureComponentId = table.Column<Guid>(nullable: true),
                    ComponentAmount = table.Column<decimal>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryProcessedDataComponents",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeSalaryProcessedDataId = table.Column<Guid>(nullable: true),
                    EmployeeSalaryComponentId = table.Column<Guid>(nullable: true),
                    BenefitDeductionId = table.Column<Guid>(nullable: true),
                    ComponentValue = table.Column<decimal>(nullable: true),
                    Type = table.Column<string>(maxLength: 10, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryProcessedDataComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryProcessedDatas",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    FinancialYearId = table.Column<Guid>(nullable: true),
                    MonthCycleId = table.Column<Guid>(nullable: true),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true),
                    PositionId = table.Column<Guid>(nullable: true),
                    GradeId = table.Column<Guid>(nullable: true),
                    TotalDaysInMonth = table.Column<short>(nullable: true),
                    TotalWorkingDays = table.Column<short>(nullable: true),
                    TotalPresentDays = table.Column<short>(nullable: true),
                    TotalAbsentDays = table.Column<short>(nullable: true),
                    TotalLeaveDays = table.Column<short>(nullable: true),
                    WeeklyOffDays = table.Column<short>(nullable: true),
                    GovernmentOffDays = table.Column<short>(nullable: true),
                    TotalWorkingHoliday = table.Column<short>(nullable: true),
                    OTHour = table.Column<decimal>(nullable: true),
                    OTRate = table.Column<decimal>(nullable: true),
                    GrossSalary = table.Column<decimal>(nullable: true),
                    PayableSalary = table.Column<decimal>(nullable: true),
                    TotalDeductionAmount = table.Column<decimal>(nullable: true),
                    NetPayableAmount = table.Column<decimal>(nullable: true),
                    ProcessDate = table.Column<DateTime>(nullable: true),
                    StampCost = table.Column<short>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmloyeeSalaryId = table.Column<Guid>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApprovedBy = table.Column<Guid>(nullable: true),
                    ApprovedTime = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryProcessedDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOptions",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PaymentOptionId = table.Column<short>(nullable: true),
                    OptionName = table.Column<string>(maxLength: 50, nullable: true),
                    OptionCode = table.Column<string>(maxLength: 10, nullable: true),
                    SortOrder = table.Column<short>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryStructureComponents",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SalaryStrutureId = table.Column<Guid>(nullable: true),
                    SalaryComponentName = table.Column<string>(maxLength: 50, nullable: true),
                    Value = table.Column<decimal>(nullable: true),
                    ValueType = table.Column<string>(maxLength: 5, nullable: true),
                    PercentOn = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryStructureComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryStructures",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StructureName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryStructures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankBranches",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeBankAccounts",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeSalaries",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryComponents",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryProcessedDataComponents",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryProcessedDatas",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "PaymentOptions",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "SalaryStructureComponents",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "SalaryStructures",
                schema: "payroll");
        }
    }
}
