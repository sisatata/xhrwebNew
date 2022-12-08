using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeFilterSpecification : BaseSpecification<Employee>
    {
        public EmployeeFilterSpecification(Guid companyId, DateTime StartDate, DateTime EndDate)
            : base(x => (x.QuitDate == null || x.QuitDate > EndDate) && x.JoiningDate <= EndDate
            && x.CompanyId == companyId)
        {

        }
    }
}