using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {

        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.Property(hr => hr.Comment).HasMaxLength(4000);
            builder.Property(hr => hr.IpAddress).HasMaxLength(20);
            builder.Property(hr => hr.EntityName).HasMaxLength(100);
        }
    }
}

