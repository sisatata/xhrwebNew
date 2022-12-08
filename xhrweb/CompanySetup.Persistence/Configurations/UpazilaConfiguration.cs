using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class UpazilaConfiguration : IEntityTypeConfiguration<Upazila>
    {
        public void Configure(EntityTypeBuilder<Upazila> builder)
        {
            builder.Property(hr => hr.UpazilaName).HasMaxLength(50);
            builder.Property(hr => hr.UpazilaNameLocalized).HasMaxLength(50);
        }
    }
}

