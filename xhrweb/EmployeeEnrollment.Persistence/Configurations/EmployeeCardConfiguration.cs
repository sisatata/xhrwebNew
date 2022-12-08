using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeCardConfiguration : IEntityTypeConfiguration<EmployeeCard>
    {

        public void Configure(EntityTypeBuilder<EmployeeCard> builder)
        {
            builder.Property(hr => hr.CardMaskingValue).HasMaxLength(20);
        }
    }
}

