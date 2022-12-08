using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeeDeviceFilterSpecification : BaseSpecification<EmployeeDevice>
    {
        public EmployeeDeviceFilterSpecification(Guid employeeId, string accessToken)
            : base(r => r.AccessToken == accessToken)
        {

        }
    }
}
