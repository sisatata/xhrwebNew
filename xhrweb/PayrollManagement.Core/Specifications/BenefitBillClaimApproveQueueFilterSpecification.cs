using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
   public class BenefitBillClaimApproveQueueFilterSpecification : BaseSpecification<BenefitBillClaimApproveQueue>
    {
        public BenefitBillClaimApproveQueueFilterSpecification(Guid benefitBillClaimId, Guid managerId)
            : base(x => x.BenefitBillClaimId == benefitBillClaimId && x.ManagerId == managerId)
        {
        }
    }
}
