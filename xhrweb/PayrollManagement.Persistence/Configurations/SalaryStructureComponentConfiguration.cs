using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class SalaryStructureComponentConfiguration : IEntityTypeConfiguration<SalaryStructureComponent>
    {

        public void Configure(EntityTypeBuilder<SalaryStructureComponent> builder)
        {
            builder.Property(hr => hr.SalaryComponentName).HasMaxLength(50);
            builder.Property(hr => hr.ValueType).HasMaxLength(5);
            builder.Property(hr => hr.PercentOn).HasMaxLength(50);


        }
    }
}

