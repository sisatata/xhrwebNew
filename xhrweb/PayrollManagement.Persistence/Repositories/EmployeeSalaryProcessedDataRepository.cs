using ASL.Hrms.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using PayrollManagement.Core.Entities.EmployeeSalaryProcessAggregate;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Persistence.Repositories
{
    public class EmployeeSalaryProcessedDataRepository : IEmployeeSalaryProcessedDataRepository
    {
        public EmployeeSalaryProcessedDataRepository(PayrollContext payrollContext)
        {
            _payrollContext = payrollContext;
        }

        private readonly PayrollContext _payrollContext;

        public async Task Update(EmployeeSalaryProcessedDataAggregate employeeSalaryProcessedData)
        {
            foreach (var item in employeeSalaryProcessedData.EmployeeSalaryProcessedDataList)
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

            foreach (var component in employeeSalaryProcessedData.EmployeeSalaryProcessedDataComponentList)
            {
                if (component.State == TrackingState.Added)
                {
                    _payrollContext.Entry(component).State = EntityState.Added;
                }
                if (component.State == TrackingState.Modified)
                {
                    _payrollContext.Entry(component).State = EntityState.Modified;
                }
                if (component.State == TrackingState.Deleted)
                {
                    _payrollContext.Entry(component).State = EntityState.Deleted;
                }
            }
            if (employeeSalaryProcessedData.EmployeePaidIncomeTaxList != null && employeeSalaryProcessedData.EmployeePaidIncomeTaxList.Count > 0)
            {
                foreach (var component in employeeSalaryProcessedData.EmployeePaidIncomeTaxList)
                {
                    if (component.State == TrackingState.Added)
                    {
                        _payrollContext.Entry(component).State = EntityState.Added;
                    }
                    if (component.State == TrackingState.Modified)
                    {
                        _payrollContext.Entry(component).State = EntityState.Modified;
                    }
                    if (component.State == TrackingState.Deleted)
                    {
                        _payrollContext.Entry(component).State = EntityState.Deleted;
                    }
                }
            }

            if (employeeSalaryProcessedData.EmployeePFTransactionList != null && employeeSalaryProcessedData.EmployeePFTransactionList.Count > 0)
            {
                foreach (var component in employeeSalaryProcessedData.EmployeePFTransactionList)
                {
                    if (component.State == TrackingState.Added)
                    {
                        _payrollContext.Entry(component).State = EntityState.Added;
                    }
                    if (component.State == TrackingState.Modified)
                    {
                        _payrollContext.Entry(component).State = EntityState.Modified;
                    }
                    if (component.State == TrackingState.Deleted)
                    {
                        _payrollContext.Entry(component).State = EntityState.Deleted;
                    }
                }
            }

            await _payrollContext.SaveChangesAsync();
        }
    }
}
