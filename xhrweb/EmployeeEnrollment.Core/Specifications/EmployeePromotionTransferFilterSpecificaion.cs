using ASL.Hrms.SharedKernel.Specifications;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Specifications
{
    public class EmployeePromotionTransferFilterSpecificaion : BaseSpecification<EmployeePromotionTransfer>

    {
        public EmployeePromotionTransferFilterSpecificaion(Guid companyId, Guid employeeId)
            : base(x => (x.PreviousCompanyId == companyId || x.NewCompanyId == companyId) && x.EmployeeId == employeeId && x.IsDeleted == false)
        {
        }
    }
}
