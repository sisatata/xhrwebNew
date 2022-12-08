using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance2Configuration : IEntityTypeConfiguration<Attendance2>
    {

        public void Configure(EntityTypeBuilder<Attendance2> builder)
        {
            builder.ToTable<Attendance2>("Attendance2");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);
        }
    }
}

