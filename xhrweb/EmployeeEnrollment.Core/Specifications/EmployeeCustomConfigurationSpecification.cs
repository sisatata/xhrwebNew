using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeCustomConfigurationSpecification : BaseSpecification<EmployeeEnrollment.Core.Entities.EmployeeCustomConfiguration>
    {
        public EmployeeCustomConfigurationSpecification(Guid employeeId, string code)
            : base(r => r.EmployeeId == employeeId && r.IsDeleted == false && r.Code == code
            && DateTime.Now.Date >= r.StartDate && DateTime.Now.Date <= r.EndDate)
        {

        }
    }
}
