using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftAllocationByEmployeeFilterSpecification : BaseSpecification<ShiftAllocation>
    {
        public ShiftAllocationByEmployeeFilterSpecification(Guid companyId, Guid employeeId)
            : base(x => x.CompanyId == companyId && x.EmployeeId == employeeId && x.IsDeleted == false)
        {

        }
    }
}