using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class SalaryRuleDetailConfiguration : IEntityTypeConfiguration<SalaryRuleDetail>
    {
        public void Configure(EntityTypeBuilder<SalaryRuleDetail> builder)
        {
            builder.Property(hr => hr.SalaryHead).HasMaxLength(50);
            builder.Property(hr => hr.ValueType).HasMaxLength(2);
            builder.Property(hr => hr.PercentDependOn).HasMaxLength(20);
        }
    }
}

