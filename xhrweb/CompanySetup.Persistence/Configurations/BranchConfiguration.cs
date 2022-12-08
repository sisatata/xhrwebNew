using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(b => b.CompanyId).IsRequired();
            builder.Property(hr => hr.BranchName).HasMaxLength(250).IsRequired();
            builder.Property(hr => hr.ShortName).HasMaxLength(20);
            builder.Property(hr => hr.BranchLocalizedName).HasMaxLength(250);
        }
    }
}
