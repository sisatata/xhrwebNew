using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {

        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.Property(hr => hr.BankName).HasMaxLength(50);
            builder.Property(hr => hr.BankNameLC).HasMaxLength(50);


        }
    }
}

