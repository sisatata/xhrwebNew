using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitDeductionIntervalConfiguration : IEntityTypeConfiguration<BenefitDeductionInterval>
    {

        public void Configure(EntityTypeBuilder<BenefitDeductionInterval> builder)
        {
            builder.Property(hr => hr.IntervalName).HasMaxLength(50);


        }
    }
}

