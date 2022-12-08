using CompanySetup.Core.Entities.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(hr => hr.CompanyName).HasMaxLength(250).IsRequired();
            builder.Property(hr => hr.ShortName).HasMaxLength(20);
            builder.Property(hr => hr.CompanyLocalizedName).HasMaxLength(250);
            builder.Property(hr => hr.CompanySlogan).HasMaxLength(150);
            builder.Property(hr => hr.LicenseKey).HasMaxLength(150);
            builder.Property(hr => hr.Notes).HasMaxLength(250);
            builder.Property(hr => hr.ApprovalStatus).HasMaxLength(3);
            builder.Property(hr => hr.CompanyMaskingId).HasMaxLength(20);
        }
    }
}
