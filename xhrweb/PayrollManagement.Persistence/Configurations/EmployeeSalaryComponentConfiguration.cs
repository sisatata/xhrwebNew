using PayrollManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayrollManagement.Persistence.Configurations
{

    public class EmployeeSalaryComponentConfiguration : IEntityTypeConfiguration<EmployeeSalaryComponent>
    {

        public void Configure(EntityTypeBuilder<EmployeeSalaryComponent> builder)
        {


        }
    }
}

