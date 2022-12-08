using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeePFTransactionConfiguration : IEntityTypeConfiguration<EmployeePFTransaction>
    {
        public void Configure(EntityTypeBuilder<EmployeePFTransaction> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
        }
    }
}

