using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class AttendanceRequestConfiguration : IEntityTypeConfiguration<AttendanceRequest>
    {

        public void Configure(EntityTypeBuilder<AttendanceRequest> builder)
        {
            builder.Property(hr => hr.Reason).HasMaxLength(250);
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(50);
            builder.Property(hr => hr.Note).HasMaxLength(250);
        }
    }
}

