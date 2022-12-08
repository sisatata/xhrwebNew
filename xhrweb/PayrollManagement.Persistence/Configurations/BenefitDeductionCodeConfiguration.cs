using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitDeductionCodeConfiguration : IEntityTypeConfiguration<BenefitDeductionCode>
    {

        public void Configure(EntityTypeBuilder<BenefitDeductionCode> builder)
        {
            builder.Property(hr => hr.BenifitDeductionCode).HasMaxLength(50);
            builder.Property(hr => hr.BenifitDeductionCodeName).HasMaxLength(100);


        }
    }
}

