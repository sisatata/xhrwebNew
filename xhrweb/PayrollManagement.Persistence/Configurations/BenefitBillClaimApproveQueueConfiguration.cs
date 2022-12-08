using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitBillClaimApproveQueueConfiguration : IEntityTypeConfiguration<BenefitBillClaimApproveQueue>
    {

        public void Configure(EntityTypeBuilder<BenefitBillClaimApproveQueue> builder)
        {
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
            builder.Property(hr => hr.Note).HasMaxLength(100);


        }
    }
}

