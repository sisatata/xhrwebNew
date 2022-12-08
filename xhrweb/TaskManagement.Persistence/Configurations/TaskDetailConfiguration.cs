using TaskManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Persistence.Configurations
{

    public class TaskDetailConfiguration : IEntityTypeConfiguration<TaskDetail>
    {
        public void Configure(EntityTypeBuilder<TaskDetail> builder)
        {
            builder.Property(hr => hr.TaskName).HasMaxLength(250);
            builder.Property(hr => hr.TaskDescription).HasMaxLength(1024);
        }
    }
}

