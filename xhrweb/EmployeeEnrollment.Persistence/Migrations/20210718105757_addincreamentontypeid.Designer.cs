﻿// <auto-generated />
using System;
using EmployeeEnrollment.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeEnrollment.Persistence.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20210718105757_addincreamentontypeid")]
    partial class addincreamentontypeid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("employee")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<string>("FullName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FullNameLC")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("GenderId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LoginId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("NationalityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("AddressLine2")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<Guid?>("AddressTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("PostOffice")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("ThanaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Village")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EmployeeAddresses");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CardMaskingValue")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<bool>("CardRevoked")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CardRevokedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("ChargeAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("IssueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("EmployeeCards");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeCustomConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CustomValue")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("EmployeeCustomConfigurations");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BID")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("BloodGroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("FathersName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("MaritalStatusId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MothersName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NID")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("ReligionId")
                        .HasColumnType("uuid");

                    b.Property<string>("SpouseName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EmployeeDetails");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessToken")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Device")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OSVersion")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("OperatingSystem")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeDevices");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeEducation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BoardorUniversity")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EducationalDegreeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EducationalInstituteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("OutOf")
                        .HasColumnType("numeric");

                    b.Property<string>("PassingYear")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Result")
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.Property<string>("ResultType")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Session")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EmployeeEducations");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("EmployeeEmails");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeEnrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ConfirmationId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EmployeeCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmployeeImageUri")
                        .HasColumnType("text");

                    b.Property<Guid?>("EmployeeTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GenderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GradeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("JobTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("JoiningDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("PermanentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("QuitDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("SubGradeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeEnrollments");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyAddress")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("CompanyContactNo")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("CompanyEmail")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyMobile")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("CompanyName")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("CompanyWeb")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Department")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Designation")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FunctionalDesignation")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("TilDate")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("EmployeeExperiences");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeFamilyMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EducationClass")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("EducationYear")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("EducationalInstitute")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("FamiliyMemberName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDependant")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEligibleForEducation")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEligibleForMedical")
                        .HasColumnType("boolean");

                    b.Property<Guid>("RelationTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeFamilyMembers");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeImageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FamilyMemberId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeImages");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AssignDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("AssignedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimaryManager")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ManagerTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("UnAssignDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UnAssignedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeManagers");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("DisplayToEmployee")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("DownloadId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Note")
                        .HasColumnType("character varying(2048)")
                        .HasMaxLength(2048);

                    b.HasKey("Id");

                    b.ToTable("EmployeeNotes");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeePhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("PhoneTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeePhones");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeePromotionTransfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.Property<Guid?>("ApproveBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ApproveDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ApproveNote")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("IncrementAmount")
                        .HasColumnType("numeric");

                    b.Property<int?>("IncrementOn")
                        .HasColumnType("integer");

                    b.Property<int?>("IncrementOnId")
                        .HasColumnType("integer");

                    b.Property<int?>("IncrementTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("IncrementValue")
                        .HasColumnType("numeric");

                    b.Property<int?>("IncrementValueTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("NewBasic")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("NewBranchId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NewCompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NewDepartmentId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("NewGross")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("NewHouserent")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("NewPositionId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("PreviousBasic")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("PreviousBranchId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreviousCompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreviousDepartmentId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("PreviousGross")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("PreviousHouserent")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("PreviousPositionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ProposedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Reason")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Reference")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("EmployeePromotionTransfers");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasMaxLength(10);

                    b.Property<string>("EmployeeStatusName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("EmployeeStatusNameLC")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<short?>("Rank")
                        .HasColumnType("smallint");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatuses");
                });

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatusHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
