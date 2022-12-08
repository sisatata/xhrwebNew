﻿// <auto-generated />
using System;
using CompanySetup.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CompanySetup.Persistence.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20210415103110_activitylogentitymodify")]
    partial class activitylogentitymodify
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("main")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CompanySetup.Core.Entities.ActivityLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActivityLogTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("character varying(4000)")
                        .HasMaxLength(4000);

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("EntityName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IpAddress")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.ActivityLogType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SystemKeyword")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ActivityLogTypes");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BranchLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ShortName")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<long>("SortOrder")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CommonLookUpType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LookUpTypeName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LookUpTypeNameLC")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ShortCode")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<short>("SortOrder")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("CommonLookUpTypes");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CompanyAddress", b =>
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

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DistrictId")
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

                    b.ToTable("CompanyAddresses");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CompanyAggregate.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.Property<string>("CompanyLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CompanyLogoUri")
                        .HasColumnType("text");

                    b.Property<string>("CompanyMaskingId")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CompanySlogan")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("CompanyWebsite")
                        .HasColumnType("text");

                    b.Property<string>("FacebookLink")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LicenseKey")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Notes")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("ParentCompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("ShortName")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<long>("SortOrder")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CompanyEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("CompanyEmails");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CompanyPhone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid?>("PhoneTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("CompanyPhones");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.ConfirmationRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<short?>("ConfirmationMonths")
                        .HasColumnType("smallint");

                    b.Property<short?>("ConfirmationType")
                        .HasColumnType("smallint");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RuleName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<short?>("SortOrder")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ConfirmationRules");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.CustomConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CustomValue")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("CustomConfigurations");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.DefaultConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("DefaultConfigurations");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("DepartmentLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ShortName")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<long>("SortOrder")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Designation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DesignationLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DesignationName")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LinkedEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("LinkedEntityType")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<long>("SortOrder")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DivisionId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<float?>("Latitude")
                        .HasColumnType("real");

                    b.Property<float?>("Longitude")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NameLocalized")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Website")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.FinancialYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FinancialYearEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FinancialYearLocalizedName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FinancialYearName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("FinancialYearStartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRunningYear")
                        .HasColumnType("boolean");

                    b.Property<short>("SortOrder")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("FinancialYears");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("GradeName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("GradeNameLocalized")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.MonthCycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FinancialYearId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("boolean");

                    b.Property<string>("MonthCycleLocalizedName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MonthCycleName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("MonthEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("MonthStartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("RunningFlag")
                        .HasColumnType("boolean");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.Property<double>("TotalWorkingDays")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("MonthCycles");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ApplicantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsViewed")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NotificationDetail")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<Guid?>("NotificationOwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("NotificationTitle")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<short?>("NotificationTypeId")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("ViewedTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.OfficeNotice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicableSections")
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Details")
                        .HasColumnType("character varying(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGeneral")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSectionSpecific")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("OfficeNotices");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.SalaryRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RuleName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("SalaryRules");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.SalaryRuleDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PercentDependOn")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("SalaryHead")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("SalaryRuleId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Value")
                        .HasColumnType("numeric");

                    b.Property<string>("ValueType")
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("SalaryRuleDetails");
                });

            modelBuilder.Entity("CompanySetup.Core.Entities.Upazila", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("UpazilaName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UpazilaNameLocalized")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Upazilas");
                });
#pragma warning restore 612, 618
        }
    }
}
