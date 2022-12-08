using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionInterval.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBenefitDeductionInterval : IRequest<BenefitDeductionIntervalCommandVM>
            {
                public Int32 IntervalId { get; set; }
                public string IntervalName { get; set; }
            }

            public class UpdateBenefitDeductionInterval : IRequest<BenefitDeductionIntervalCommandVM>
            {
                public Guid Id { get; set; }
                public Int32 IntervalId { get; set; }
                public string IntervalName { get; set; }
            }

            public class MarkAsDeleteBenefitDeductionInterval : IRequest<BenefitDeductionIntervalCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
