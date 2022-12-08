using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class DefaultConfigurationConfiguration : IEntityTypeConfiguration<DefaultConfiguration>
    {

        public void Configure(EntityTypeBuilder<DefaultConfiguration> builder)
        {
            builder.Property(hr => hr.Code).HasMaxLength(50);
            builder.Property(hr => hr.DefaultValue).HasMaxLength(100);
            builder.Property(hr => hr.Description).HasMaxLength(250);


        }
    }
}

