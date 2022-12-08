using TaskManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Persistence.Configurations
{
    public class TaskCategoryConfiguration : IEntityTypeConfiguration<TaskCategory>
    {
        public void Configure(EntityTypeBuilder<TaskCategory> builder)
        {
            builder.Property(hr => hr.TaskCategoryName).HasMaxLength(150);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);
        }
    }
}

