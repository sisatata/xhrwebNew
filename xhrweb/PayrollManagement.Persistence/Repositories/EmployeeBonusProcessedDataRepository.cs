using ASL.Hrms.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Persistence.Repositories
{
    public class EmployeeBonusProcessedDataRepository : IEmployeeBonusProcessedDataRepository
    {
        public EmployeeBonusProcessedDataRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        private readonly PayrollContext _payrollContext;

        public async Task Update(EmployeeBonusProcessAggregate employeeBonusProcessAggregate)
        {
            foreach (var item in employeeBonusProcessAggregate.EmployeeBonusProcessedDataList)
            {
                if (item.State == TrackingState.Added)
                {
                    _payrollContext.Entry(item).State = EntityState.Added;
                }
                if (item.State == TrackingState.Modified)
                {
                    _payrollContext.Entry(item).State = EntityState.Modified;
                }
                if (item.State == TrackingState.Deleted)
                {
                    _payrollContext.Entry(item).State = EntityState.Deleted;
                }
            }

            await _payrollContext.SaveChangesAsync();
        }
    }
}
