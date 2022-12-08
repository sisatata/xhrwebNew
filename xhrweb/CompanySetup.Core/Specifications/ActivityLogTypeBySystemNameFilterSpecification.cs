using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class ActivityLogTypeBySystemNameFilterSpecification : BaseSpecification<ActivityLogType>
    {
        public ActivityLogTypeBySystemNameFilterSpecification(string systemKeyword)

            : base(x => x.SystemKeyword == systemKeyword && x.Enabled == true)
        {

        }
    }
}