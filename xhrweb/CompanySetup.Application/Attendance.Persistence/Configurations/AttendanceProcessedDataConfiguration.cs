using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{

    public class AttendanceProcessedDataConfiguration : IEntityTypeConfiguration<AttendanceProcessedData>
    {

        public void Configure(EntityTypeBuilder<AttendanceProcessedData> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.PunchYear).HasMaxLength(10);
            builder.Property(hr => hr.ShiftCode).HasMaxLength(3);
            builder.Property(hr => hr.Status).HasMaxLength(3);
            builder.Property(hr => hr.Status_V2).HasMaxLength(3);
            builder.Property(hr => hr.Remarks).HasMaxLength(500);
            builder.Property(hr => hr.EmployeeRemarks).HasMaxLength(500);
            builder.Property(hr => hr.ClockInAddress).HasMaxLength(500);
            builder.Property(hr => hr.ClockOutAddress).HasMaxLength(500);
        }
    }
}

