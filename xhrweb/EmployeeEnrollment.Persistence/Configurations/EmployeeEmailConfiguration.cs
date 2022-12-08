using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeEmailConfiguration : IEntityTypeConfiguration<EmployeeEmail>
    {

        public void Configure(EntityTypeBuilder<EmployeeEmail> builder)
        {
            builder.Property(hr => hr.EmailAddress).HasMaxLength(100);
        }
    }
}

