using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class AttendanceProcessedDataFilterSpecification : BaseSpecification<AttendanceProcessedData>
    {
        public AttendanceProcessedDataFilterSpecification(Guid companyId, DateTime startDate, DateTime endDate)
            : base(x => x.CompanyId == companyId && x.PunchDate >= startDate && x.PunchDate <= endDate)
        {

        }
    }
}