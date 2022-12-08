using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeStatusConfiguration : IEntityTypeConfiguration<EmployeeStatus>
    {

        public void Configure(EntityTypeBuilder<EmployeeStatus> builder)
        {
            builder.Property(hr => hr.EmployeeStatusName).HasMaxLength(50);
            builder.Property(hr => hr.EmployeeStatusNameLC).HasMaxLength(50);
            builder.Property(hr => hr.CompanyId).HasMaxLength(10);


        }
    }
}

