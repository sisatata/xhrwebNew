using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitBillClaimConfiguration : IEntityTypeConfiguration<BenefitBillClaim>
    {

        public void Configure(EntityTypeBuilder<BenefitBillClaim> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.Status).HasMaxLength(3);
            builder.Property(hr => hr.ApprovedNotes).HasMaxLength(250);
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
            builder.Property(hr => hr.BillAttachmentName).HasMaxLength(50);
            builder.Property(hr => hr.LocationFrom).HasMaxLength(250);
            builder.Property(hr => hr.LocationTo).HasMaxLength(250);
            builder.Property(hr => hr.NameOfAttendees).HasMaxLength(250);
            builder.Property(hr => hr.BillNo).ValueGeneratedOnAdd();
            builder.Property(hr => hr.BillNoMaskingValue).HasMaxLength(30);
        }
    }
}

