using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class CompanyWiseConfigurationAllFilterSpecification : BaseSpecification<CompanyWiseConfiguration>
    {
        public CompanyWiseConfigurationAllFilterSpecification()

            : base(x => x.Id != null)
        {

        }
    }
}