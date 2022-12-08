using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class ShiftAllocationNoFilterSpecification : BaseSpecification<ShiftAllocation>
    {
        public ShiftAllocationNoFilterSpecification() : base(x => x.IsDeleted == false)
        {

        }
    }
}