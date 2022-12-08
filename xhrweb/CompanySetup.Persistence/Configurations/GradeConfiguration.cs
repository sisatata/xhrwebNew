using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(hr => hr.GradeName).HasMaxLength(50);
            builder.Property(hr => hr.GradeNameLocalized).HasMaxLength(50);
        }
    }
}

