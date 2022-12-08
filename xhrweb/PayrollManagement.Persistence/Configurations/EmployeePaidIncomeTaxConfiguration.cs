using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeePaidIncomeTaxConfiguration : IEntityTypeConfiguration<EmployeePaidIncomeTax>
    {

        public void Configure(EntityTypeBuilder<EmployeePaidIncomeTax> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.FinancialYear).HasMaxLength(20);
            builder.Property(hr => hr.MonthName).HasMaxLength(20);
            builder.Property(hr => hr.Remarks).HasMaxLength(500);
        }
    }
}

