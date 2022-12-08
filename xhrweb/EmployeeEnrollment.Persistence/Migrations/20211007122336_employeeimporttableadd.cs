using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeEnrollment.Persistence.Migrations
{
    public partial class employeeimporttableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeRawDataHists",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeCode = table.Column<string>(maxLength: 250, nullable: true),
                    Gross = table.Column<string>(maxLength: 250, nullable: true),
                    FullName = table.Column<string>(maxLength: 250, nullable: true),
                    NID = table.Column<string>(maxLength: 250, nullable: true),
                    BID = table.Column<string>(maxLength: 250, nullable: true),
                    MobileNo = table.Column<string>(maxLength: 250, nullable: true),
                    JoiningDate = table.Column<string>(maxLength: 250, nullable: true),
                    DOB = table.Column<string>(maxLength: 250, nullable: true),
                    Gender = table.Column<string>(maxLength: 250, nullable: true),
                    Nationality = table.Column<string>(maxLength: 250, nullable: true),
                    NightBill = table.Column<string>(maxLength: 250, nullable: true),
                    OT = table.Column<string>(maxLength: 250, nullable: true),
                    Religion = table.Column<string>(maxLength: 250, nullable: true),
                    Division = table.Column<string>(maxLength: 250, nullable: true),
                    Department = table.Column<string>(maxLength: 250, nullable: true),
                    Section = table.Column<string>(maxLength: 250, nullable: true),
                    StaffCategory = table.Column<string>(maxLength: 250, nullable: true),
                    Company = table.Column<string>(maxLength: 250, nullable: true),
                    Country = table.Column<string>(maxLength: 250, nullable: true),
                    MaritalStatus = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<string>(maxLength: 1, nullable: true),
                    BloodGroup = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddress = table.Column<string>(maxLength: 500, nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    BankName = table.Column<string>(maxLength: 250, nullable: true),
                    BankAccount = table.Column<string>(maxLength: 250, nullable: true),
                    FullNameBangla = table.Column<string>(maxLength: 250, nullable: true),
                    NameofFather = table.Column<string>(maxLength: 250, nullable: true),
                    NameofSpouce = table.Column<string>(maxLength: 250, nullable: true),
                    NameofMother = table.Column<string>(maxLength: 250, nullable: true),
                    planetEmployeeID = table.Column<Guid>(nullable: true),
                    Position = table.Column<string>(maxLength: 250, nullable: true),
                    ErrorDescription = table.Column<string>(maxLength: 250, nullable: true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    MoveDate = table.Column<DateTime>(nullable: false),
                    JobType = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentDate = table.Column<string>(maxLength: 250, nullable: true),
                    Emailaddress = table.Column<string>(maxLength: 250, nullable: true),
                    EmergencyContact = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentDistrict = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentThana = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentPostOffice = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentVillage = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentAddressLine1 = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentAddressLine2 = table.Column<string>(maxLength: 250, nullable: true),
                    PresentDistrict = table.Column<string>(maxLength: 250, nullable: true),
                    PresentThana = table.Column<string>(maxLength: 250, nullable: true),
                    PresentPostOffice = table.Column<string>(maxLength: 250, nullable: true),
                    PresentVillage = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddressLine1 = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddressLine2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRawDataHists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRawDataPreps",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeCode = table.Column<string>(maxLength: 250, nullable: true),
                    Gross = table.Column<string>(maxLength: 250, nullable: true),
                    FullName = table.Column<string>(maxLength: 250, nullable: true),
                    NID = table.Column<string>(maxLength: 250, nullable: true),
                    BID = table.Column<string>(maxLength: 250, nullable: true),
                    MobileNo = table.Column<string>(maxLength: 250, nullable: true),
                    JoiningDate = table.Column<string>(maxLength: 250, nullable: true),
                    DOB = table.Column<string>(maxLength: 250, nullable: true),
                    Gender = table.Column<string>(maxLength: 250, nullable: true),
                    Nationality = table.Column<string>(maxLength: 250, nullable: true),
                    NightBill = table.Column<string>(maxLength: 250, nullable: true),
                    OT = table.Column<string>(maxLength: 250, nullable: true),
                    Religion = table.Column<string>(maxLength: 250, nullable: true),
                    Division = table.Column<string>(maxLength: 250, nullable: true),
                    Department = table.Column<string>(maxLength: 250, nullable: true),
                    Section = table.Column<string>(maxLength: 250, nullable: true),
                    StaffCategory = table.Column<string>(maxLength: 250, nullable: true),
                    Company = table.Column<string>(maxLength: 250, nullable: true),
                    Country = table.Column<string>(maxLength: 250, nullable: true),
                    MaritalStatus = table.Column<string>(maxLength: 250, nullable: true),
                    BloodGroup = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddress = table.Column<string>(maxLength: 500, nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 250, nullable: true),
                    BankName = table.Column<string>(maxLength: 250, nullable: true),
                    BankAccount = table.Column<string>(maxLength: 250, nullable: true),
                    FullNameBangla = table.Column<string>(maxLength: 250, nullable: true),
                    NameofFather = table.Column<string>(maxLength: 250, nullable: true),
                    NameofSpouce = table.Column<string>(maxLength: 250, nullable: true),
                    NameofMother = table.Column<string>(maxLength: 250, nullable: true),
                    PlanetEmployeeID = table.Column<Guid>(nullable: true),
                    Position = table.Column<string>(maxLength: 250, nullable: true),
                    ErrorDescription = table.Column<string>(maxLength: 250, nullable: true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    JobType = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentDate = table.Column<string>(maxLength: 250, nullable: true),
                    Emailaddress = table.Column<string>(maxLength: 250, nullable: true),
                    EmergencyContact = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentDistrict = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentThana = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentPostOffice = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentVillage = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentAddressLine1 = table.Column<string>(maxLength: 250, nullable: true),
                    PermanentAddressLine2 = table.Column<string>(maxLength: 250, nullable: true),
                    PresentDistrict = table.Column<string>(maxLength: 250, nullable: true),
                    PresentThana = table.Column<string>(maxLength: 250, nullable: true),
                    PresentPostOffice = table.Column<string>(maxLength: 250, nullable: true),
                    PresentVillage = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddressLine1 = table.Column<string>(maxLength: 250, nullable: true),
                    PresentAddressLine2 = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRawDataPreps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRawDataHists",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeRawDataPreps",
                schema: "employee");
        }
    }
}
