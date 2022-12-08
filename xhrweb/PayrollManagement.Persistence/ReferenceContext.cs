using Microsoft.EntityFrameworkCore;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
           : base(options)
        { }
        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
        public DbSet<MonthCycle> MonthCycles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AttendanceCalendar> AttendanceCalendars { get; set; }
        public DbSet<AttendanceProcessedData> AttendanceProcessedDatas { get; set; }
        public DbSet<EmployeeSalaryComponentVM> EmployeeSalaryComponentVMs { get; set; }
        public DbSet<EmployeeCustomConfiguration> EmployeeCustomConfigurations { get; set; }
        public DbSet<CommonLookUpType> CommonLookUpTypes { get; set; }

        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<FinancialYearMonth> FinancialYearMonths { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payroll");
            modelBuilder.Entity<EmployeeManager>().ToTable("EmployeeManagers", "leave");
            modelBuilder.Entity<MonthCycle>().ToTable("MonthCycles", "payroll");
            modelBuilder.Entity<Employee>().ToTable("Employees", "payroll");
            modelBuilder.Entity<AttendanceProcessedData>().ToTable("AttendanceProcessedDatas", "payroll");
            modelBuilder.Entity<AttendanceCalendar>().ToTable("AttendanceCalendars", "payroll");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.Year).HasColumnName("FinancialYearName");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.YearStartDate).HasColumnName("FinancialYearStartDate");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.YearEndDate).HasColumnName("FinancialYearEndDate");

            modelBuilder.Entity<EmployeeSalaryComponentVM>().ToTable("EmployeeSalaryComponentVMs", "payroll");
            modelBuilder.Entity<EmployeeCustomConfiguration>().ToTable("EmployeeWiseCustomConfigurations", "payroll");

            modelBuilder.Entity<CommonLookUpType>().ToTable("CommonLookUpTypes", "main");

            modelBuilder.Entity<LeaveBalance>().ToTable("LeaveBalance", "leave");
            modelBuilder.Entity<FinancialYearMonth>().ToTable("FinancialYearMonth", "leave");
            modelBuilder.Entity<LeaveType>().ToTable("LeaveTypes", "leave");

            base.OnModelCreating(modelBuilder);
        }
    }
}
