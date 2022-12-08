using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeBonusProcessedDataConfiguration : IEntityTypeConfiguration<EmployeeBonusProcessedData>
    {

        public void Configure(EntityTypeBuilder<EmployeeBonusProcessedData> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.Remarks).HasMaxLength(500);
        }
    }
}

