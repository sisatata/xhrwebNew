using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class MonthCycleNoFilterSpecification : BaseSpecification<MonthCycle>
    {
        public MonthCycleNoFilterSpecification()
            : base(x => x.Id == x.Id)
        {

        }
    }
}