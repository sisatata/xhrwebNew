using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Persistence.Configurations
{
    class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.Ignore(lb => lb.State);
            builder.Property(lb => lb.CompanyId).IsRequired();
            builder.Property(lb => lb.EmployeeId).IsRequired();
            builder.Property(lb => lb.LeaveTypeId).IsRequired();
            builder.Property(lb => lb.LeaveCalendar).HasMaxLength(20).IsRequired();
        }
    }
}
