using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance12Configuration : IEntityTypeConfiguration<Attendance12>
    {

        public void Configure(EntityTypeBuilder<Attendance12> builder)
        {
            builder.ToTable<Attendance12>("Attendance12");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);

        }
    }
}

