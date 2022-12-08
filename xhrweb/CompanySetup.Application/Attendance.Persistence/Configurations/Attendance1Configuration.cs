using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance1Configuration : IEntityTypeConfiguration<Attendance1>
    {

        public void Configure(EntityTypeBuilder<Attendance1> builder)
        {
            builder.ToTable<Attendance1>("Attendance1");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);
        }
    }
}

