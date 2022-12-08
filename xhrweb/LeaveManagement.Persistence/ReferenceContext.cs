using LeaveManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
        public DbSet<LeaveCalendar> LeaveCalendars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
        public DbSet<EmployeeCustomConfiguration> EmployeeCustomConfigurations { get; set; }
        public DbSet<AttendanceProcessedData> AttendanceProcessedDatas { get; set; }
        public DbSet<FinancialYearMonth> FinancialYearMonths { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");
            modelBuilder.Entity<LeaveCalendar>().ToTable("FinancialYears");
            modelBuilder.Entity<LeaveCalendar>().Property(c => c.Year).HasColumnName("FinancialYearName");
            modelBuilder.Entity<LeaveCalendar>().Property(c => c.YearStartDate).HasColumnName("FinancialYearStartDate");
            modelBuilder.Entity<LeaveCalendar>().Property(c => c.YearEndDate).HasColumnName("FinancialYearEndDate");

            modelBuilder.Entity<Employee>().ToTable("EmployeeInfo", "leave");
            modelBuilder.Entity<EmployeeManager>().ToTable("EmployeeManagers", "leave");

            modelBuilder.Entity<EmployeeCustomConfiguration>().ToTable("EmployeeWiseCustomConfigurations", "leave");

            modelBuilder.Entity<AttendanceProcessedData>().ToTable("AttendanceProcessedDatas", "leave");

            modelBuilder.Entity<FinancialYearMonth>().ToTable("FinancialYearMonth", "leave");

            base.OnModelCreating(modelBuilder);
        }
    }

}
