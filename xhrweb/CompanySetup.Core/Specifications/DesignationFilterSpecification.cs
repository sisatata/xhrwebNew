using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class DesignationFilterSpecification : BaseSpecification<Designation>
    {
        public DesignationFilterSpecification()

            : base(x => x.IsDeleted == false)
        {

        }
    }
}