using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{
    public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(b => b.LinkedEntityId).IsRequired();
            builder.Property(d => d.LinkedEntityType).HasMaxLength(50).IsRequired();
            builder.Property(d => d.DesignationName).HasMaxLength(250).IsRequired();
            builder.Property(hr => hr.ShortName).HasMaxLength(20);
            builder.Property(hr => hr.DesignationLocalizedName).HasMaxLength(250);
        }
    }
}
