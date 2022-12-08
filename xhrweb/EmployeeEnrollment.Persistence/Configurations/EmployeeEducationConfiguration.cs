using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeEducationConfiguration : IEntityTypeConfiguration<EmployeeEducation>
    {

        public void Configure(EntityTypeBuilder<EmployeeEducation> builder)
        {
            builder.Property(hr => hr.Session).HasMaxLength(20);
            builder.Property(hr => hr.PassingYear).HasMaxLength(20);
            builder.Property(hr => hr.BoardorUniversity).HasMaxLength(150);
            builder.Property(hr => hr.Result).HasMaxLength(30);
            builder.Property(hr => hr.ResultType).HasMaxLength(10);
        }
    }
}

