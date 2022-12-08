using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionCode.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBenefitDeductionCode : IRequest<BenefitDeductionCodeCommandVM>
            {
                public Guid? CompanyId { get; set; }
                public string BenifitDeductionCode { get; set; }
                public string BenifitDeductionCodeName { get; set; }
            }

            public class UpdateBenefitDeductionCode : IRequest<BenefitDeductionCodeCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? CompanyId { get; set; }
                public string BenifitDeductionCode { get; set; }
                public string BenifitDeductionCodeName { get; set; }
            }

            public class MarkAsDeleteBenefitDeductionCode : IRequest<BenefitDeductionCodeCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
