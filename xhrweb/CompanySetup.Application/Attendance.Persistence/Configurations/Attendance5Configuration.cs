using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance5Configuration : IEntityTypeConfiguration<Attendance5>
    {

        public void Configure(EntityTypeBuilder<Attendance5> builder)
        {
            builder.ToTable<Attendance5>("Attendance5");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);

        }
    }
}

