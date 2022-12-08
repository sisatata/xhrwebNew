using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class SalaryStructureComponentFilterSpecificaion : BaseSpecification<SalaryStructureComponent>

    {
        public SalaryStructureComponentFilterSpecificaion(Guid structureId)
            : base(x => x.SalaryStrutureId == structureId && x.IsDeleted == false)
        {
        }
    }
}
