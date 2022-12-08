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
    [Migration("20200415083524_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("main")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

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

            modelBuilder.Entity("CompanySetup.Core.Entities.CompanyAggregate.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyLocalizedName")
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<string>("CompanySlogan")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LicenseKey")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

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
#pragma warning restore 612, 618
        }
    }
}