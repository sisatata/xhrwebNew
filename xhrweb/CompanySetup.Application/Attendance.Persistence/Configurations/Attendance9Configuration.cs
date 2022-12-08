using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance9Configuration : IEntityTypeConfiguration<Attendance9>
    {

        public void Configure(EntityTypeBuilder<Attendance9> builder)
        {
            builder.ToTable<Attendance9>("Attendance9");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);
        }
    }
}

