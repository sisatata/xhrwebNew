using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class ActivityLogTypeConfiguration : IEntityTypeConfiguration<ActivityLogType>
    {

        public void Configure(EntityTypeBuilder<ActivityLogType> builder)
        {
            builder.Property(hr => hr.SystemKeyword).HasMaxLength(100);
            builder.Property(hr => hr.Name).HasMaxLength(200);
        }
    }
}

