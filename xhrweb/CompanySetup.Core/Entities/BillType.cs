using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Entities
{
    public class BillType
    {
        public Guid? BenefitDeductionId { get; private set; }
        public string BillTypeName { get; private set; }
        public Guid? CompanyId { get; private set; }
        public decimal AllocatedAmount { get; set; }

    }
}
