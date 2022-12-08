using ASL.AccessControl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.AccessControl.Identity
{
    public class ReferenceContext : DbContext
    {
        public ReferenceContext(DbContextOptions<ReferenceContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("access");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);

            modelBuilder.Entity<Employee>().ToTable("EmployeeInfo", "access");
            modelBuilder.Entity<Company>().ToTable("Company", "main");
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

