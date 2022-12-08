using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeSalaryProcessedDataConfiguration : IEntityTypeConfiguration<EmployeeSalaryProcessedData>
    {

        public void Configure(EntityTypeBuilder<EmployeeSalaryProcessedData> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.Remarks).HasMaxLength(150);
        }
    }
}

