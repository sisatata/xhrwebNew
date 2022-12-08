using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance7Configuration : IEntityTypeConfiguration<Attendance7>
    {

        public void Configure(EntityTypeBuilder<Attendance7> builder)
        {
            builder.ToTable<Attendance7>("Attendance7");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);

        }
    }
}

