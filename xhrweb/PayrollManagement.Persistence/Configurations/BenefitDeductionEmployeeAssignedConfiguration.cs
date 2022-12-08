using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class BenefitDeductionEmployeeAssignedConfiguration : IEntityTypeConfiguration<BenefitDeductionEmployeeAssigned>
    {

        public void Configure(EntityTypeBuilder<BenefitDeductionEmployeeAssigned> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(250);


        }
    }
}

