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
   public class EmployeeDevicesRepository: IEmployeeDevicesRepository
    {
        public EmployeeDevicesRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        private readonly EmployeeContext _employeeContext;

        public async Task Update(List<EmployeeDevice> employeeDevices)
        {
            foreach (var empDevice in employeeDevices)
            {
                if (empDevice.State == TrackingState.Added)
                {
                    _employeeContext.Entry(empDevice).State = EntityState.Added;
                }
                if (empDevice.State == TrackingState.Modified)
                {
                    _employeeContext.Entry(empDevice).State = EntityState.Modified;
                }
                if (empDevice.State == TrackingState.Deleted)
                {
                    _employeeContext.Entry(empDevice).State = EntityState.Deleted;
                }
            }
            await _employeeContext.SaveChangesAsync();
        }
    }
}