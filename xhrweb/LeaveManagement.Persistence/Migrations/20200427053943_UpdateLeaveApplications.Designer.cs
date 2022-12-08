﻿// <auto-generated />
using System;
using LeaveManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LeaveManagement.Persistence.Migrations
{
    [DbContext(typeof(LeaveContext))]
    [Migration("20200427053943_UpdateLeaveApplications")]
    partial class UpdateLeaveApplications
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("leave")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LeaveManagement.Core.Entities.LeaveApplicationAggregate.LeaveApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ApplyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LeaveApplicationStatus")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LeaveCalendar")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uuid");

                    b.Property<double>("NoOfDays")
                        .HasColumnType("double precision");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("LeaveApplications");
                });

            modelBuilder.Entity("LeaveManagement.Core.Entities.LeaveBalanceAggregate.LeaveBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LeaveCalendar")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uuid");

                    b.Property<double>("MaximumBalance")
                        .HasColumnType("double precision");

                    b.Property<double>("RemainingBalance")
                        .HasColumnType("double precision");

                    b.Property<double>("UsedBalance")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("LeaveBalance");
                });

            modelBuilder.Entity("LeaveManagement.Core.Entities.LeaveSetupAggregate.LeaveType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("ConsecutiveLimit")
                        .HasColumnType("integer");

                    b.Property<int>("EncashReserveBalance")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAllowAdvanceLeaveApply")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAllowOpeningBalance")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAllowWithPrecedingHoliday")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBasedWorkingDays")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCarryForward")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDependOnDateOfConfirmation")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEncashable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFemaleSpecific")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLeaveDaysFixed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPreventPostLeaveApply")
                        .HasColumnType("boolean");

                    b.Property<string>("LeaveTypeLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("LeaveTypeName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("LeaveTypeShortName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LeaveTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
