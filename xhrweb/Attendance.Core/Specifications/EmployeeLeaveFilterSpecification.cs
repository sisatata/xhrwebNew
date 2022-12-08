using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeLeaveFilterSpecification : BaseSpecification<EmployeeLeave>
    {
        public EmployeeLeaveFilterSpecification(Guid companyId, DateTime StartDate, DateTime EndDate)

            : base(x => x.CompanyId == companyId &&  ( StartDate>= x.StartDate || StartDate<= x.EndDate ) || (EndDate >= x.StartDate || EndDate <= x.EndDate))
        {

        }
    }
}