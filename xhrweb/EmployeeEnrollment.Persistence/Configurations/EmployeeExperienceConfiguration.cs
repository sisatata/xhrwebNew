using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeExperienceConfiguration : IEntityTypeConfiguration<EmployeeExperience>
    {

        public void Configure(EntityTypeBuilder<EmployeeExperience> builder)
        {
            builder.Property(hr => hr.CompanyName).HasMaxLength(150);
            builder.Property(hr => hr.Designation).HasMaxLength(150);
            builder.Property(hr => hr.FunctionalDesignation).HasMaxLength(150);
            builder.Property(hr => hr.Department).HasMaxLength(150);
            builder.Property(hr => hr.Responsibilities).HasMaxLength(500);
            builder.Property(hr => hr.CompanyAddress).HasMaxLength(150);
            builder.Property(hr => hr.CompanyContactNo).HasMaxLength(20);
            builder.Property(hr => hr.CompanyMobile).HasMaxLength(20);
            builder.Property(hr => hr.CompanyEmail).HasMaxLength(150);
            builder.Property(hr => hr.CompanyWeb).HasMaxLength(50);
        }
    }
}

