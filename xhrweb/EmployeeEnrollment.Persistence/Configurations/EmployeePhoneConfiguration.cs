using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeePhoneConfiguration : IEntityTypeConfiguration<EmployeePhone>
    {

        public void Configure(EntityTypeBuilder<EmployeePhone> builder)
        {
            builder.Property(hr => hr.PhoneNumber).HasMaxLength(20);


        }
    }
}

