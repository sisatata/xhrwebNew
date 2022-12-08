using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Persistence.Configurations
{
    public class ShiftGroupConfiguration : IEntityTypeConfiguration<ShiftGroup>
    {
        public void Configure(EntityTypeBuilder<ShiftGroup> builder)
        {
            builder.Property(hr => hr.ShiftGroupName).IsRequired().HasMaxLength(150);
            builder.Property(hr => hr.ShiftGroupNameLC).HasMaxLength(150);
            builder.Property(hr => hr.CompanyId).IsRequired();
            builder.Property(hr => hr.WeekendNames).HasMaxLength(250);
        }
    }
}

