using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeLeaveEncashmentConfiguration : IEntityTypeConfiguration<EmployeeLeaveEncashment>
    {

        public void Configure(EntityTypeBuilder<EmployeeLeaveEncashment> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.EncashDate).HasMaxLength(10);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
        }
    }
}

