using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
        public DbSet<AttendanceCalendar> LeaveCalendars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; } 
        public DbSet<AttendanceCommon> AttendanceCommons { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Branch> Branch { get; set; }

        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
        public DbSet<EmployeeCard> EmployeeCards { get; set; }
        public DbSet<MonthCycle> MonthCycles { get; set; }
        public DbSet<EmployeeCustomConfiguration> EmployeeCustomConfigurations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");
            modelBuilder.Entity<AttendanceCalendar>().ToTable("FinancialYears");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.Year).HasColumnName("FinancialYearName");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.YearStartDate).HasColumnName("FinancialYearStartDate");
            modelBuilder.Entity<AttendanceCalendar>().Property(c => c.YearEndDate).HasColumnName("FinancialYearEndDate");

            modelBuilder.Entity<Employee>().ToTable("EmployeeInfo", "attendance");
            modelBuilder.Entity<EmployeeLeave>().ToTable("EmployeeLeaves", "attendance");
            modelBuilder.Entity<AttendanceCommon>().ToTable("AttendanceCommon", "attendance");
            modelBuilder.Entity<EmployeeManager>().ToTable("EmployeeManagers", "leave");
            modelBuilder.Entity<MonthCycle>().ToTable("MonthCycles", "attendance");
            modelBuilder.Entity<Company>().ToTable("Company", "attendance");

            modelBuilder.Entity<EmployeeCard>().ToTable("EmployeeCards", "employee");
            modelBuilder.Entity<EmployeeCustomConfiguration>().ToTable("EmployeeWiseCustomConfigurations", "payroll");


            base.OnModelCreating(modelBuilder);
        }
    }

}
