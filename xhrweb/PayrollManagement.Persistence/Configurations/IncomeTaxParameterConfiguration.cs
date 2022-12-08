using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class IncomeTaxParameterConfiguration : IEntityTypeConfiguration<IncomeTaxParameter>
    {

        public void Configure(EntityTypeBuilder<IncomeTaxParameter> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
            builder.Property(hr => hr.PayerTypeCode).HasMaxLength(20);


        }
    }
}

