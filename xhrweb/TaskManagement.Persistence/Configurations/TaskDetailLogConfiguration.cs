using TaskManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Persistence.Configurations
{

    public class TaskDetailLogConfiguration : IEntityTypeConfiguration<TaskDetailLog>
    {

        public void Configure(EntityTypeBuilder<TaskDetailLog> builder)
        {
            builder.Property(hr => hr.UpdateInfo).HasMaxLength(1024);
        }
    }
}

