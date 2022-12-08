using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
           : base(options)
        { }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<EmployeeSalaryComponent> EmployeeSalaryComponents { get; set; }
        //public DbSet<SalaryStructure> SalaryStructures { get; set; }
        public DbSet<SalaryStructureComponent> SalaryStructureComponents { get; set; }
        public DbSet<FinancialYearMonth> FinancialYearMonths { get; set; }
        public DbSet<CompanyOrganogram> CompanyOrganograms { get; set; }
        public DbSet<CompanyWiseConfiguration> CompanyWiseConfigurations { get; set; }
        public DbSet<CommonLookUpType> CommonLookUpTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<LeaveTypeGroup> LeaveTypeGroups { get; set; }
        public DbSet<ConfirmationRule> ConfirmationRules { get; set; }
        public DbSet<SalaryStructure> SalaryStructures { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("employee");
            modelBuilder.Entity<EmployeeSalary>().ToTable("EmployeeSalaries", "payroll");
            modelBuilder.Entity<FinancialYearMonth>().ToTable("FinancialYearMonth", "main");
            modelBuilder.Entity<EmployeeSalaryComponent>().ToTable("EmployeeSalaryComponents", "payroll");
            modelBuilder.Entity<SalaryStructureComponent>().ToTable("SalaryStructureComponents", "payroll");
            modelBuilder.Entity<CompanyWiseConfiguration>().ToTable("CompanyWiseConfigurations", "main");
            modelBuilder.Entity<CompanyOrganogram>().ToTable("CompanyOrganograms");
            modelBuilder.Entity<CommonLookUpType>().ToTable("CommonLookUpTypes", "main");
            modelBuilder.Entity<Grade>().ToTable("Grades", "main");
            modelBuilder.Entity<LeaveTypeGroup>().ToTable("LeaveTypeGroups", "leave");
            modelBuilder.Entity<ConfirmationRule>().ToTable("ConfirmationRules", "main");
            modelBuilder.Entity<SalaryStructure>().ToTable("SalaryStructures", "payroll");
            modelBuilder.Entity<PaymentOption>().ToTable("PaymentOptions", "payroll");

            modelBuilder.Entity<District>().ToTable("Districts", "main");
            modelBuilder.Entity<Upazila>().ToTable("Upazilas", "main");

            base.OnModelCreating(modelBuilder);
        }
    }
}