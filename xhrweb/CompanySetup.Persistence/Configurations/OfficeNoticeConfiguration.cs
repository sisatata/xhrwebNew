using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class OfficeNoticeConfiguration : IEntityTypeConfiguration<OfficeNotice>
    {

        public void Configure(EntityTypeBuilder<OfficeNotice> builder)
        {
            builder.Property(hr => hr.Subject).HasMaxLength(250);
            builder.Property(hr => hr.Details).HasMaxLength(2000);
            builder.Property(hr => hr.ApplicableSections);
        }
    }
}

