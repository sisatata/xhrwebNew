using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeBankAccountConfiguration : IEntityTypeConfiguration<EmployeeBankAccount>
    {

        public void Configure(EntityTypeBuilder<EmployeeBankAccount> builder)
        {
            builder.Property(hr => hr.AccountNo).HasMaxLength(20);
            builder.Property(hr => hr.AccountTitle).HasMaxLength(100);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);


        }
    }
}

