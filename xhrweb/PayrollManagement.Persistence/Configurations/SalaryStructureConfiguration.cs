using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class SalaryStructureConfiguration : IEntityTypeConfiguration<SalaryStructure>
    {

        public void Configure(EntityTypeBuilder<SalaryStructure> builder)
        {
            builder.Property(hr => hr.StructureName).HasMaxLength(50);
            builder.Property(hr => hr.Description).HasMaxLength(250);
        }
    }
}

