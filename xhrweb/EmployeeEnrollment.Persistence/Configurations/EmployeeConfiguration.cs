using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(hr => hr.EmployeeId).HasMaxLength(15);
            builder.Property(hr => hr.FullName).HasMaxLength(50);
            builder.Property(hr => hr.FullNameLC).HasMaxLength(50);
            builder.Property(hr => hr.ReferenceNumber).HasMaxLength(15);
        }
    }
}