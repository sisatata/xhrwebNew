using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeImageConfiguration : IEntityTypeConfiguration<EmployeeImage>
    {

        public void Configure(EntityTypeBuilder<EmployeeImage> builder)
        {
            //builder.Property(hr => hr.Photo).HasMaxLength(-1);


        }
    }
}

