using PayrollManagement.Application.BenefitDeductionInterval.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BenefitDeductionInterval.Queries
{
    public static class Queries
    {
        public class GetBenefitDeductionIntervalList : IRequest<List<BenefitDeductionIntervalVM>>
        {
            //public Guid CompanyId { get; set; }
        }

        public class GetBenefitDeductionInterval : IRequest<BenefitDeductionIntervalVM>
        {
            public Guid Id { get; set; }
        }
    }
}
