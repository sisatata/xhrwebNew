using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.NotificationTitle).HasMaxLength(250);
            builder.Property(hr => hr.NotificationDetail).HasMaxLength(1000);
        }
    }
}

