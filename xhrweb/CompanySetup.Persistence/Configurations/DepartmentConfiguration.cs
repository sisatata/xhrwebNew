using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(b => b.CompanyId).IsRequired();
            builder.Property(d => d.BranchId).IsRequired();
            builder.Property(hr => hr.DepartmentName).HasMaxLength(250).IsRequired();            
            builder.Property(hr => hr.DepartmentLocalizedName).HasMaxLength(250);
            builder.Property(hr => hr.ShortName).HasMaxLength(20);
        }
    }
}
