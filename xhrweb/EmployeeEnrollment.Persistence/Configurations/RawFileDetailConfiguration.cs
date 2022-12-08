using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class RawFileDetailConfiguration : IEntityTypeConfiguration<RawFileDetail>
    {

        public void Configure(EntityTypeBuilder<RawFileDetail> builder)
        {
            builder.Property(hr => hr.FileName).HasMaxLength(250);
            builder.Property(hr => hr.FileType).HasMaxLength(50);
            builder.Property(hr => hr.Comments).HasMaxLength(250);
            builder.Property(hr => hr.Status).HasMaxLength(1);
        }
    }
}

