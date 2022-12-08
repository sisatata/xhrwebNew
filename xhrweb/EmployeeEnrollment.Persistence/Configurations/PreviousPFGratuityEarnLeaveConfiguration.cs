using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class PreviousPFGratuityEarnLeaveConfiguration : IEntityTypeConfiguration<PreviousPFGratuityEarnLeave>
    {

        public void Configure(EntityTypeBuilder<PreviousPFGratuityEarnLeave> builder)
        {
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
        }
    }
}

