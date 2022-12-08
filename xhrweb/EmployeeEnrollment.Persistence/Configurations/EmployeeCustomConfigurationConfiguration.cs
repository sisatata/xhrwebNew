using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeCustomConfigurationConfiguration : IEntityTypeConfiguration<EmployeeCustomConfiguration>
    {

        public void Configure(EntityTypeBuilder<EmployeeCustomConfiguration> builder)
        {
            builder.Property(hr => hr.Code).HasMaxLength(50);
            builder.Property(hr => hr.CustomValue).HasMaxLength(100);
            builder.Property(hr => hr.Description).HasMaxLength(250);


        }
    }
}

