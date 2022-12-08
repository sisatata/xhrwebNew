using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetail>
    {

        public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
        {
            builder.Property(hr => hr.FathersName).HasMaxLength(50);
            builder.Property(hr => hr.MothersName).HasMaxLength(50);
            builder.Property(hr => hr.SpouseName).HasMaxLength(50);
            builder.Property(hr => hr.NID).HasMaxLength(20);
            builder.Property(hr => hr.BID).HasMaxLength(20);


        }
    }
}

