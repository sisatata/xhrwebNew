using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeSalaryConfiguration : IEntityTypeConfiguration<EmployeeSalary>
    {

        public void Configure(EntityTypeBuilder<EmployeeSalary> builder)
        {


        }
    }
}

