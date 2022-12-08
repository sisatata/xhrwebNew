using PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries
{
    public static class Queries
    {
        public class GetBenefitDeductionEmployeeAssignedList : IRequest<List<BenefitDeductionEmployeeAssignedVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBenefitDeductionEmployeeAssigned : IRequest<BenefitDeductionEmployeeAssignedVM>
        {
            public Guid Id { get; set; }
        }
    }
}
