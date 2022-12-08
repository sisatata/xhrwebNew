using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {

        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.Property(hr => hr.Name).HasMaxLength(50);
            builder.Property(hr => hr.NameLocalized).HasMaxLength(50);
            builder.Property(hr => hr.Website).HasMaxLength(150);
        }
    }
}

