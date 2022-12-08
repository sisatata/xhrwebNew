using LeaveManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Persistence.Configurations
{

    public class LeaveTypeGroupConfiguration : IEntityTypeConfiguration<LeaveTypeGroup>
    {

        public void Configure(EntityTypeBuilder<LeaveTypeGroup> builder)
        {
            builder.Property(hr => hr.Id).ValueGeneratedOnAdd();
            builder.Property(hr => hr.LeaveTypeGroupName).HasMaxLength(50);
            builder.Property(hr => hr.LeaveTypeGroupNameLC).HasMaxLength(50);
        }
    }
}

