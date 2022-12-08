using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Core.Interfaces
{
    public interface IEmployeeDevicesRepository
    {
        Task Update(List<EmployeeDevice> employeeDevices);
    }
}
