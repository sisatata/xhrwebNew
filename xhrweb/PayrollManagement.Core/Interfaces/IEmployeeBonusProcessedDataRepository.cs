using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Interfaces
{
    public interface IEmployeeBonusProcessedDataRepository
    {
        Task Update(EmployeeBonusProcessAggregate employeeBonusProcessAggregate);
    }
}
