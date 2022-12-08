using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class HolidayFilterByDateSpecification : BaseSpecification<Holiday>
    {
        public HolidayFilterByDateSpecification(DateTime holidayDate)

            : base(x => x.IsDeleted == false && x.HolidayDate.Date == holidayDate)
        {

        }
    }
}