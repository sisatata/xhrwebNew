using PayrollManagement.Application.BenefitDeductionConfig.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BenefitDeductionConfig.Queries
{
    public static class Queries
    {
        public class GetBenefitDeductionConfigList : IRequest<List<BenefitDeductionConfigVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBenefitDeductionConfig : IRequest<BenefitDeductionConfigVM>
        {
            public Guid Id { get; set; }
        }
    }
}
