using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BankBranchConfiguration : IEntityTypeConfiguration<BankBranch>
    {
        public void Configure(EntityTypeBuilder<BankBranch> builder)
        {
            builder.Property(hr => hr.BranchName).HasMaxLength(50);
            builder.Property(hr => hr.BranchNameLC).HasMaxLength(50);
            builder.Property(hr => hr.BranchAddress).HasMaxLength(250);
            builder.Property(hr => hr.ContactPerson).HasMaxLength(50);
            builder.Property(hr => hr.ContactNumber).HasMaxLength(20);
            builder.Property(hr => hr.ContactEmailId).HasMaxLength(50);
        }
    }
}

