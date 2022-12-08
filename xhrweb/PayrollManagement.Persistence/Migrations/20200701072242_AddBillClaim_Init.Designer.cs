﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PayrollManagement.Persistence;

namespace PayrollManagement.Persistence.Migrations
{
    [DbContext(typeof(PayrollContext))]
    [Migration("20200701072242_AddBillClaim_Init")]
    partial class AddBillClaim_Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("payroll")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PayrollManagement.Core.Entities.BenefitBillClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("AllocatedAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("ApprovedAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("ApprovedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ApprovedNotes")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("BenefitDeductionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BillDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("ClaimAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("ClaimDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Status")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("BenefitBillClaims");
                });

            modelBuilder.Entity("PayrollManagement.Core.Entities.BenefitDeductionCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BenifitDeductionCode")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("BenifitDeductionCodeName")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("BenefitDeductionCodes");
                });

            modelBuilder.Entity("PayrollManagement.Core.Entities.BenefitDeductionConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BasicOrGross")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<string>("BenefitDeductionCode")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CalculationType")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal?>("FixedAmount")
                        .HasColumnType("numeric");

                    b.Property<int?>("IntervalId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCalculateSalary")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<decimal?>("PercentOfBasicOrGross")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("BenefitDeductionConfigs");
                });

            modelBuilder.Entity("PayrollManagement.Core.Entities.BenefitDeductionEmployeeAssigned", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("BenefitDeductionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Remarks")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("BenefitDeductionEmployeeAssigneds");
                });

            modelBuilder.Entity("PayrollManagement.Core.Entities.BenefitDeductionInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("IntervalId")
                        .HasColumnType("integer");

                    b.Property<string>("IntervalName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("BenefitDeductionIntervals");
                });
#pragma warning restore 612, 618
        }
    }
}
