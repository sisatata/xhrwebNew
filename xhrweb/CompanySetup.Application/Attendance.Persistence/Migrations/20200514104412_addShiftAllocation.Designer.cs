﻿// <auto-generated />
using System;
using Attendance.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Attendance.Persistence.Migrations
{
    [DbContext(typeof(AttendanceContext))]
    [Migration("20200514104412_addShiftAllocation")]
    partial class addShiftAllocation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("attendance")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Attendance.Core.Entities.Shift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ShiftName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Attendance.Core.Entities.ShiftAllocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("C1")
                        .HasColumnType("text");

                    b.Property<string>("C10")
                        .HasColumnType("text");

                    b.Property<string>("C11")
                        .HasColumnType("text");

                    b.Property<string>("C12")
                        .HasColumnType("text");

                    b.Property<string>("C13")
                        .HasColumnType("text");

                    b.Property<string>("C14")
                        .HasColumnType("text");

                    b.Property<string>("C15")
                        .HasColumnType("text");

                    b.Property<string>("C16")
                        .HasColumnType("text");

                    b.Property<string>("C17")
                        .HasColumnType("text");

                    b.Property<string>("C18")
                        .HasColumnType("text");

                    b.Property<string>("C19")
                        .HasColumnType("text");

                    b.Property<string>("C2")
                        .HasColumnType("text");

                    b.Property<string>("C20")
                        .HasColumnType("text");

                    b.Property<string>("C21")
                        .HasColumnType("text");

                    b.Property<string>("C22")
                        .HasColumnType("text");

                    b.Property<string>("C23")
                        .HasColumnType("text");

                    b.Property<string>("C24")
                        .HasColumnType("text");

                    b.Property<string>("C25")
                        .HasColumnType("text");

                    b.Property<string>("C26")
                        .HasColumnType("text");

                    b.Property<string>("C27")
                        .HasColumnType("text");

                    b.Property<string>("C28")
                        .HasColumnType("text");

                    b.Property<string>("C29")
                        .HasColumnType("text");

                    b.Property<string>("C3")
                        .HasColumnType("text");

                    b.Property<string>("C30")
                        .HasColumnType("text");

                    b.Property<string>("C31")
                        .HasColumnType("text");

                    b.Property<string>("C4")
                        .HasColumnType("text");

                    b.Property<string>("C5")
                        .HasColumnType("text");

                    b.Property<string>("C6")
                        .HasColumnType("text");

                    b.Property<string>("C7")
                        .HasColumnType("text");

                    b.Property<string>("C8")
                        .HasColumnType("text");

                    b.Property<string>("C9")
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("DutyMonth")
                        .HasColumnType("integer");

                    b.Property<int>("DutyYear")
                        .HasColumnType("integer");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FinancialYearId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MonthCycleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PrimaryShiftId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RotationDay")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ShiftAllocations");
                });

            modelBuilder.Entity("Attendance.Core.Entities.ShiftGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ShiftGroupName")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("ShiftGroupNameLC")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("ShiftGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
