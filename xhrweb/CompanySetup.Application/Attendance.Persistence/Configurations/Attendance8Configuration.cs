using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance8Configuration : IEntityTypeConfiguration<Attendance8>
    {

        public void Configure(EntityTypeBuilder<Attendance8> builder)
        {
            builder.ToTable<Attendance8>("Attendance8");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);

        }
    }
}

