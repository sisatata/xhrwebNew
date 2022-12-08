using Attendance.Core.Entities;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Persistence.Configurations
{
    public class ShiftAllocationConfiguration : IEntityTypeConfiguration<ShiftAllocation>
    {
        public void Configure(EntityTypeBuilder<ShiftAllocation> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.EmployeeId).IsRequired();

            builder.Property(hr => hr.CompanyId).IsRequired();
            builder.Property(hr => hr.DutyYear).HasMaxLength(20);
            builder.Property(hr => hr.DutyMonth).HasMaxLength(20);
        }
    }
}
