using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {

        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.Property(hr => hr.Reason).HasMaxLength(100);
            builder.Property(hr => hr.ReasonLocalized).HasMaxLength(100);
        }
    }
}

