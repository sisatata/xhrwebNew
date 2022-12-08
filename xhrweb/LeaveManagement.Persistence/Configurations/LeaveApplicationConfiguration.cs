using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Persistence.Configurations
{
    public class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
    {
        public void Configure(EntityTypeBuilder<LeaveApplication> builder)
        {
            //builder.Property(lp => lp.LeaveApplicationStatus).HasConversion<string>()
            //    .HasMaxLength(50);
            builder.Property(lp => lp.LeaveCalendar).HasMaxLength(50);
        }
    }
}
