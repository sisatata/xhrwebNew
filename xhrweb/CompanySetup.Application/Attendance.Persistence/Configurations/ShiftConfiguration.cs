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
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.Ignore(hr => hr.StartRange);
            builder.Ignore(hr => hr.EndRange);
            builder.Property(hr => hr.ShiftName).IsRequired().HasMaxLength(50);
            builder.Property(hr => hr.ShiftLocalizedName).HasMaxLength(50);
            builder.Property(hr => hr.ShiftCode).IsRequired();
            //builder.Property(hr => hr.StartDate).IsRequired();
            //builder.Property(hr=> hr.EndDate).IsRequired();
        }
    }
}
