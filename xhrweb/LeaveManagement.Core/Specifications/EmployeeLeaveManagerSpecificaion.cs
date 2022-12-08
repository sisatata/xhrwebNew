using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
    public class EmployeeLeaveManagerSpecificaion : BaseSpecification<EmployeeManager>

    {
        public EmployeeLeaveManagerSpecificaion(Guid employeeId)
            : base(x => x.EmployeeId == employeeId)
        {
        }
    }
}
