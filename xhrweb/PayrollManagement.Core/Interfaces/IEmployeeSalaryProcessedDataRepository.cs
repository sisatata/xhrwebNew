using PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Interfaces
{
    public interface IEmployeeSalaryProcessedDataRepository
    {
        Task Update(EmployeeSalaryProcessedDataAggregate employeeSalaryProcessedData);
    }
}
