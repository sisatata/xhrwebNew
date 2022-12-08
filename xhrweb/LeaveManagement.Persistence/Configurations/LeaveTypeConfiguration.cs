using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(c => c.LeaveTypeName).HasMaxLength(250);
            builder.Property(c => c.LeaveTypeLocalizedName).HasMaxLength(250);
            builder.Property(c => c.LeaveTypeShortName).HasMaxLength(50);
        }
    }
}
