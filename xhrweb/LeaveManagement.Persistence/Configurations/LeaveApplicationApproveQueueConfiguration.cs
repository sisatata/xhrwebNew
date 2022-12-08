using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Persistence.Configurations
{

    public class LeaveApplicationApproveQueueConfiguration : IEntityTypeConfiguration<LeaveApplicationApproveQueue>
    {
        public void Configure(EntityTypeBuilder<LeaveApplicationApproveQueue> builder)
        {
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
            builder.Property(hr => hr.Note).HasMaxLength(100);
        }
    }
}

