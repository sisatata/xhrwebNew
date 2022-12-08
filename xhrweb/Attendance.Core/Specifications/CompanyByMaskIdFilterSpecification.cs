using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class CompanyByMaskIdFilterSpecification : BaseSpecification<Company>
    {
        public CompanyByMaskIdFilterSpecification(string companyMaskingId)
            : base(x => x.CompanyMaskingId == companyMaskingId)
        {

        }
    }
}