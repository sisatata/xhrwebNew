using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class AttendanceProcessedEmployeeDataFilterSpecification : BaseSpecification<AttendanceProcessedData>
    {
        public AttendanceProcessedEmployeeDataFilterSpecification(Guid companyId, Guid employeeId, DateTime startDate, DateTime endDate)
            : base(x => x.CompanyId == companyId && x.PunchDate >= startDate && x.PunchDate <= endDate && x.EmployeeId == employeeId)
        {

        }
    }
}