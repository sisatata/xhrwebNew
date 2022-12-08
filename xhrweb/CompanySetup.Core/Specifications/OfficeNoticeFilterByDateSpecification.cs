using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class OfficeNoticeFilterByDateSpecification : BaseSpecification<OfficeNotice>
    {
        public OfficeNoticeFilterByDateSpecification(DateTime noticeDate)
            : base(x => x.IsDeleted == false && x.StartDate.Value.Date == noticeDate && x.IsPublished == true)
        {

        }
    }
}