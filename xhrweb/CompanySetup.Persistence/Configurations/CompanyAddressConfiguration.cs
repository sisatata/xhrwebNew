using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class CompanyAddressConfiguration : IEntityTypeConfiguration<CompanyAddress>
    {

        public void Configure(EntityTypeBuilder<CompanyAddress> builder)
        {
            builder.Property(hr => hr.AddressLine1).HasMaxLength(150);
            builder.Property(hr => hr.AddressLine2).HasMaxLength(150);
            builder.Property(hr => hr.Village).HasMaxLength(50);
            builder.Property(hr => hr.PostOffice).HasMaxLength(50);
        }
    }
}

