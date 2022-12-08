using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Entities;

namespace TaskManagement.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
        //public DbSet<LeaveCalendar> LeaveCalendars { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");


            modelBuilder.Entity<Employee>().ToTable("EmployeeInfo", "task");
            modelBuilder.Entity<EmployeeManager>().ToTable("EmployeeManagers", "task");

            base.OnModelCreating(modelBuilder);
        }
    }

}
