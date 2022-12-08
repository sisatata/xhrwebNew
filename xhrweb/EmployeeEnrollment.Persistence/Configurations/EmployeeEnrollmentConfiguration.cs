using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeEnrollmentConfiguration : IEntityTypeConfiguration<EmployeeEnrollment.Core.Entities.EmployeeEnrollment>
    {

        public void Configure(EntityTypeBuilder<EmployeeEnrollment.Core.Entities.EmployeeEnrollment> builder)
        {
            builder.Property(hr => hr.LeaveTypeGroup).HasMaxLength(50);

        }
    }
}

