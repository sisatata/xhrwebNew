using ASL.Hrms.SharedKernel.Enums;
using EmployeeEnrollment.Core.Entities;
using EmployeeEnrollment.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Persistence.Repositories
{
    public class EmployeeRawDataPrepRepository : IEmployeeRawDataPrepRepository
    {
        public EmployeeRawDataPrepRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        private readonly EmployeeContext _employeeContext;

        public async Task Save(List<EmployeeRawDataPrep> employeeRawDataPreps)
        {
            foreach (var employeeRawDataPrep in employeeRawDataPreps)
            {
                if (employeeRawDataPrep.State == TrackingState.Added)
                {
                    _employeeContext.Entry(employeeRawDataPrep).State = EntityState.Added;
                }
                if (employeeRawDataPrep.State == TrackingState.Modified)
                {
                    _employeeContext.Entry(employeeRawDataPrep).State = EntityState.Modified;
                }
                if (employeeRawDataPrep.State == TrackingState.Deleted)
                {
                    _employeeContext.Entry(employeeRawDataPrep).State = EntityState.Deleted;
                }
            }
            await _employeeContext.SaveChangesAsync();
        }
    }
}