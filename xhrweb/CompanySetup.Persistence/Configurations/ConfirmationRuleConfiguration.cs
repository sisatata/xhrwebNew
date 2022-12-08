using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class ConfirmationRuleConfiguration : IEntityTypeConfiguration<ConfirmationRule>
    {

        public void Configure(EntityTypeBuilder<ConfirmationRule> builder)
        {
            builder.Property(hr => hr.RuleName).HasMaxLength(50);
            builder.Property(hr => hr.Description).HasMaxLength(500);
        }
    }
}

