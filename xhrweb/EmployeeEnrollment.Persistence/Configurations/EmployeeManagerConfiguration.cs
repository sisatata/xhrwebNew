using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeManagerConfiguration : IEntityTypeConfiguration<EmployeeManager>
    {

        public void Configure(EntityTypeBuilder<EmployeeManager> builder)
        {


        }
    }
}

