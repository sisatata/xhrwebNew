using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class HolidayFilterSpecification : BaseSpecification<Holiday>
    {
        public HolidayFilterSpecification(Guid companyId, DateTime StartDate, DateTime EndDate)

            : base(x => x.IsDeleted == false && x.CompanyId == companyId)
        {

        }
    }
}