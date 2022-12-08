using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
    {

        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            builder.Property(hr => hr.AddressLine1).HasMaxLength(150);
            builder.Property(hr => hr.AddressLine2).HasMaxLength(150);
            builder.Property(hr => hr.Village).HasMaxLength(50);
            builder.Property(hr => hr.PostOffice).HasMaxLength(50);


        }
    }
}

