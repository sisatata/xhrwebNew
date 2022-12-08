using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeDeviceConfiguration : IEntityTypeConfiguration<EmployeeDevice>
    {
        public void Configure(EntityTypeBuilder<EmployeeDevice> builder)
        {
            builder.Ignore(lb => lb.State);
            builder.Property(hr => hr.AccessToken).HasMaxLength(250);
            builder.Property(hr => hr.Location).HasMaxLength(150);
            builder.Property(hr => hr.Device).HasMaxLength(250);
            builder.Property(hr => hr.OperatingSystem).HasMaxLength(100);
            builder.Property(hr => hr.OSVersion).HasMaxLength(50);
        }
    }
}

