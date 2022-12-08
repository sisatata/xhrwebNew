using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeSalaryProcessedDataComponentConfiguration : IEntityTypeConfiguration<EmployeeSalaryProcessedDataComponent>
    {

        public void Configure(EntityTypeBuilder<EmployeeSalaryProcessedDataComponent> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.Type).HasMaxLength(10);
            builder.Property(hr => hr.DisplayName).HasMaxLength(50);
            builder.Property(hr => hr.Description).HasMaxLength(250);
        }
    }
}

