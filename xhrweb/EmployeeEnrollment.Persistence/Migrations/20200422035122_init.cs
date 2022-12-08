using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    AddressLine1 = table.Column<string>(maxLength: 150, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 150, nullable: true),
                    Village = table.Column<string>(maxLength: 50, nullable: true),
                    PostOffice = table.Column<string>(maxLength: 50, nullable: true),
                    ThanaId = table.Column<Guid>(nullable: true),
                    DistrictId = table.Column<Guid>(nullable: true),
                    CountryId = table.Column<Guid>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    AddressTypeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    FathersName = table.Column<string>(maxLength: 50, nullable: true),
                    MothersName = table.Column<string>(maxLength: 50, nullable: true),
                    SpouseName = table.Column<string>(maxLength: 50, nullable: true),
                    MaritalStatusId = table.Column<Guid>(nullable: true),
                    ReligionId = table.Column<Guid>(nullable: true),
                    NID = table.Column<string>(maxLength: 20, nullable: true),
                    BID = table.Column<string>(maxLength: 20, nullable: true),
                    BloodGroupId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEmails",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeEmailId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(maxLength: 10, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 10, nullable: true),
                    IsPrimary = table.Column<bool>(maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEnrollments",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    QuitDate = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<Guid>(nullable: true),
                    PermanentDate = table.Column<DateTime>(nullable: true),
                    JobTypeId = table.Column<Guid>(nullable: true),
                    GradeId = table.Column<Guid>(nullable: true),
                    SubGradeId = table.Column<Guid>(nullable: true),
                    EmployeeTypeId = table.Column<Guid>(nullable: true),
                    EmployeeCategoryId = table.Column<Guid>(nullable: true),
                    ConfirmationId = table.Column<Guid>(nullable: true),
                    GenderId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEnrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeImages",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeImageId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    FamilyMemberId = table.Column<Guid>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    PhotoId = table.Column<Guid>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePhones",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeePhoneId = table.Column<Guid>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PhoneTypeId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePhones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 15, nullable: true),
                    CompanyId = table.Column<Guid>(nullable: true),
                    BranchId = table.Column<Guid>(nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: true),
                    PositionId = table.Column<Guid>(nullable: true),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    FulNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    ReferenceNumber = table.Column<string>(maxLength: 15, nullable: true),
                    NationalityId = table.Column<Guid>(nullable: true),
                    GenderId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStatuses",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeStatusId = table.Column<Guid>(nullable: false),
                    EmployeeStatusName = table.Column<string>(maxLength: 50, nullable: true),
                    EmployeeStatusNameLC = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<short>(nullable: true),
                    CompanyId = table.Column<Guid>(maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStatusHistories",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeStatusHistoryId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    ChangedDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatusHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAddresses",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeDetails",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeEmails",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeEnrollments",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeImages",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeePhones",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeStatuses",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeStatusHistories",
                schema: "employee");
        }
    }
}
