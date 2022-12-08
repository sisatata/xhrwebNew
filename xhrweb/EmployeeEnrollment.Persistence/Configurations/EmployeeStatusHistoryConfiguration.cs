using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeStatusHistoryConfiguration : IEntityTypeConfiguration<EmployeeStatusHistory>
    {

        public void Configure(EntityTypeBuilder<EmployeeStatusHistory> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(250);


        }
    }
}

