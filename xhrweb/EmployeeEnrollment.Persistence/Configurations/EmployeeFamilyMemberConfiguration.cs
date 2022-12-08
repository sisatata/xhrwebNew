using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeFamilyMemberConfiguration : IEntityTypeConfiguration<EmployeeFamilyMember>
    {

        public void Configure(EntityTypeBuilder<EmployeeFamilyMember> builder)
        {
            builder.Property(hr => hr.FamiliyMemberName).HasMaxLength(100);
            builder.Property(hr => hr.EducationClass).HasMaxLength(150);
            builder.Property(hr => hr.EducationalInstitute).HasMaxLength(150);
            builder.Property(hr => hr.EducationYear).HasMaxLength(20);


        }
    }
}

