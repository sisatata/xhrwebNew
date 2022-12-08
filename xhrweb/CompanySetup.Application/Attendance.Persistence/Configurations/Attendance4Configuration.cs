using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class Attendance4Configuration : IEntityTypeConfiguration<Attendance4>
    {

        public void Configure(EntityTypeBuilder<Attendance4> builder)
        {
            builder.ToTable<Attendance4>("Attendance4");
            builder.Property(hr => hr.AttendanceYear).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.ClockTimeAddress).HasMaxLength(500);

        }
    }
}

