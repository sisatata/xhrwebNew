using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class IncomeTaxInvestmentConfiguration : IEntityTypeConfiguration<IncomeTaxInvestment>
    {

        public void Configure(EntityTypeBuilder<IncomeTaxInvestment> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(500);


        }
    }
}

