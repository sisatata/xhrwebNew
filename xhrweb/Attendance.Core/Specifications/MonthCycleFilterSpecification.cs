using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class MonthCycleFilterSpecification : BaseSpecification<MonthCycle>
    {
        public MonthCycleFilterSpecification(Guid companyId, Guid attendanceYear, DateTime StartDate, DateTime EndDate)
            : base(x => x.FinancialYearId == attendanceYear && ((StartDate >= x.MonthStartDate && StartDate <= x.MonthEndDate)
            || (EndDate >= x.MonthStartDate && EndDate <= x.MonthEndDate)
            || (x.MonthStartDate >= StartDate && x.MonthEndDate <= EndDate)) && x.CompanyId == companyId)
        {

        }
    }
}