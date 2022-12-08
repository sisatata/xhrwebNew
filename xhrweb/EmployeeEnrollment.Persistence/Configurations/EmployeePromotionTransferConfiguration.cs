using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeePromotionTransferConfiguration : IEntityTypeConfiguration<EmployeePromotionTransfer>
    {

        public void Configure(EntityTypeBuilder<EmployeePromotionTransfer> builder)
        {
            builder.Property(hr => hr.Reason).HasMaxLength(500);
            builder.Property(hr => hr.Reference).HasMaxLength(250);
            builder.Property(hr => hr.ApproveNote).HasMaxLength(250);
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
        }
    }
}

