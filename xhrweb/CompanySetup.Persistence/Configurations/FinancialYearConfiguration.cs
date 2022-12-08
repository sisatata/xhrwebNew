using System;
using System.Collections.Generic;
using System.Text;
using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{
    public class FinancialYearConfiguration : IEntityTypeConfiguration<FinancialYear>
    {
        public void Configure(EntityTypeBuilder<FinancialYear> builder)
        {
            builder.Property(c => c.CompanyId).IsRequired();
            builder.Property(c => c.FinancialYearName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.FinancialYearLocalizedName).HasMaxLength(100);
            builder.Property(c => c.FinancialYearStartDate).IsRequired();
            builder.Property(c => c.FinancialYearEndDate).IsRequired();
           

        }
    }
}
