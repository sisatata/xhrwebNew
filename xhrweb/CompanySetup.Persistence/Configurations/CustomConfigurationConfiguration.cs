using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class CustomConfigurationConfiguration : IEntityTypeConfiguration<CustomConfiguration>
    {

        public void Configure(EntityTypeBuilder<CustomConfiguration> builder)
        {
            builder.Property(hr => hr.Code).HasMaxLength(50);
            builder.Property(hr => hr.CustomValue).HasMaxLength(100);
            builder.Property(hr => hr.Description).HasMaxLength(250);


        }
    }
}

