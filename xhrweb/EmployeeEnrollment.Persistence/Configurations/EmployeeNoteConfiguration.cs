using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeNoteConfiguration : IEntityTypeConfiguration<EmployeeNote>
    {

        public void Configure(EntityTypeBuilder<EmployeeNote> builder)
        {
            builder.Property(hr => hr.Note).HasMaxLength(2048);
        }
    }
}

