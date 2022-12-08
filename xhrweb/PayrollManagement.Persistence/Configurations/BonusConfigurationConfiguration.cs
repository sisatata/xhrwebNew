using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BonusConfigurationConfiguration : IEntityTypeConfiguration<BonusConfiguration>
    {

        public void Configure(EntityTypeBuilder<BonusConfiguration> builder)
        {
            builder.Ignore(hr => hr.BasicOrGross);
            builder.Ignore(hr => hr.PartialOn);
            //builder.Property(hr => hr.BasicOrGross).HasMaxLength(20);
            //builder.Property(hr => hr.PartialOn).HasMaxLength(50);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
        }
    }
}

