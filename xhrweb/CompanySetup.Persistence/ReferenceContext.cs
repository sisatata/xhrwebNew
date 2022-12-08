using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Persistence
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {

        }

        public DbSet<EmployeeWiseCustomConfiguration> EmployeeWiseCustomConfigurations { get; set; }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<EmployeeDevice> EmployeeDevices { get; set; }

        public DbSet<CompanyWiseConfiguration> CompanyWiseConfigurations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");
            modelBuilder.Entity<EmployeeWiseCustomConfiguration>().ToTable("EmployeeWiseCustomConfigurations", "employee");
            modelBuilder.Entity<Holiday>().ToTable("Holidays", "attendance");
            modelBuilder.Entity<EmployeeDevice>().ToTable("EmployeeDevices", "main");
            modelBuilder.Entity<CompanyWiseConfiguration>().ToTable("CompanyWiseConfigurations", "main");

            base.OnModelCreating(modelBuilder);
        }
    }
}
