using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Persistence.Configurations
{
  public  class MonthCycleConfiguration: IEntityTypeConfiguration<MonthCycle>
    {
        public void Configure(EntityTypeBuilder<MonthCycle> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(c => c.CompanyId).IsRequired();
            builder.Property(c => c.FinancialYearId).IsRequired();
            builder.Property(c => c.MonthCycleName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.MonthCycleLocalizedName).HasMaxLength(100);
            builder.Property(c => c.MonthStartDate).IsRequired();
            builder.Property(c => c.MonthEndDate).IsRequired();
            builder.Property(c => c.TotalWorkingDays).IsRequired();

        }
    }
}
