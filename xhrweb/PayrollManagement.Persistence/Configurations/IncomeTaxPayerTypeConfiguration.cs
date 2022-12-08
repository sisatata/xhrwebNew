using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class IncomeTaxPayerTypeConfiguration : IEntityTypeConfiguration<IncomeTaxPayerType>
    {

        public void Configure(EntityTypeBuilder<IncomeTaxPayerType> builder)
        {
            builder.Property(hr => hr.PayerTypeCode).HasMaxLength(20);
            builder.Property(hr => hr.PayerTypeName).HasMaxLength(50);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);


        }
    }
}

