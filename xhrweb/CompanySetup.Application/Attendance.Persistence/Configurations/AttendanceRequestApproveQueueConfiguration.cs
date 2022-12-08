using Attendance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Attendance.Persistence.Configurations
{
    public class AttendanceRequestApproveQueueConfiguration : IEntityTypeConfiguration<AttendanceRequestApproveQueue>
    {
        public void Configure(EntityTypeBuilder<AttendanceRequestApproveQueue> builder)
        {
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
            builder.Property(hr => hr.Note).HasMaxLength(100);
        }
    }
}

