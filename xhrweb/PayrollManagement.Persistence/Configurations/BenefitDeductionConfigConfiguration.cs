using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitDeductionConfigConfiguration : IEntityTypeConfiguration<BenefitDeductionConfig>
    {

        public void Configure(EntityTypeBuilder<BenefitDeductionConfig> builder)
        {
            builder.Property(hr => hr.BenefitDeductionCode).HasMaxLength(50);
            builder.Property(hr => hr.Name).HasMaxLength(50);
            builder.Property(hr => hr.Description).HasMaxLength(250);
            builder.Property(hr => hr.Type).HasMaxLength(15);
            builder.Property(hr => hr.BasicOrGross).HasMaxLength(15);
            builder.Property(hr => hr.CalculationType).HasMaxLength(15);


        }
    }
}

