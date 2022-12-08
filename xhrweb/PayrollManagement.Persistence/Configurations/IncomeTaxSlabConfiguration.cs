using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class IncomeTaxSlabConfiguration : IEntityTypeConfiguration<IncomeTaxSlab>
    {

        public void Configure(EntityTypeBuilder<IncomeTaxSlab> builder)
        {
            builder.Property(hr => hr.SlabName).HasMaxLength(250);
            builder.Property(hr => hr.Description).HasMaxLength(500);


        }
    }
}

