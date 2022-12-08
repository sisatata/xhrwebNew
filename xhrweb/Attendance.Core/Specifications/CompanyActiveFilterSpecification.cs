using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class CompanyActiveFilterSpecification : BaseSpecification<Company>
    {
        public CompanyActiveFilterSpecification()

            : base(x =>  x.ApprovalStatus == ((int)ApprovalStatuses.Approved).ToString())
        {

        }
    }
}