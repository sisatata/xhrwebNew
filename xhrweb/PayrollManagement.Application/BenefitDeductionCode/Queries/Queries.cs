using PayrollManagement.Application.BenefitDeductionCode.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BenefitDeductionCode.Queries
{
    public static class Queries
    {
        public class GetBenefitDeductionCodeList : IRequest<List<BenefitDeductionCodeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBenefitDeductionCode : IRequest<BenefitDeductionCodeVM>
        {
            public Guid Id { get; set; }
        }
    }
}
