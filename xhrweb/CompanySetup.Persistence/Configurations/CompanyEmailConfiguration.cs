using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class CompanyEmailConfiguration : IEntityTypeConfiguration<CompanyEmail>
    {

        public void Configure(EntityTypeBuilder<CompanyEmail> builder)
        {
            builder.Property(hr => hr.EmailAddress).HasMaxLength(150);
            builder.Property(hr => hr.Remarks).HasMaxLength(150);
        }
    }
}

