using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class CompanyPhoneConfiguration : IEntityTypeConfiguration<CompanyPhone>
    {

        public void Configure(EntityTypeBuilder<CompanyPhone> builder)
        {
            builder.Property(hr => hr.PhoneNumber).HasMaxLength(20);
        }
    }
}

