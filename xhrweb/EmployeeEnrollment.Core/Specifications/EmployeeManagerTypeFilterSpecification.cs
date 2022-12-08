using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeManagerTypeFilterSpecification : BaseSpecification<EmployeeManager>
    {
        public EmployeeManagerTypeFilterSpecification(Guid employeeId, Guid companyId, Guid managerId , Guid managerTypeId)
            : base(r => r.EmployeeId == employeeId && r.CompanyId == companyId && r.ManagerId == managerId && r.ManagerTypeId == managerTypeId)
        {

        }
    }
}
