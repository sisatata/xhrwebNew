// <auto-generated />
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
    [Migration("20200423131356_EmployeeEmailUpdate")]
    partial class EmployeeEmailUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("employee")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

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

                    b.Property<Guid?>("GenderId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("NationalityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PositionId")
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

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("FathersName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("MaritalStatusId")
                        .HasColumnType("uuid");

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

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasMaxLength(10);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean")
                        .HasMaxLength(10);

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

                    b.Property<Guid?>("EmployeeCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

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

                    b.Property<DateTime?>("PermanentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("QuitDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SubGradeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeEnrollments");
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

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeePhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeePhoneId")
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

            modelBuilder.Entity("EmployeeEnrollment.Core.Entities.EmployeeStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasMaxLength(10);

                    b.Property<Guid>("EmployeeStatusId")
                        .HasColumnType("uuid");

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

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeStatusHistoryId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatusHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
