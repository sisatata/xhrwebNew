using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class PaymentOptionConfiguration : IEntityTypeConfiguration<PaymentOption>
    {

        public void Configure(EntityTypeBuilder<PaymentOption> builder)
        {
            builder.Property(hr => hr.OptionName).HasMaxLength(50);
            builder.Property(hr => hr.OptionCode).HasMaxLength(10);


        }
    }
}

