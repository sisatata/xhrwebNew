using PayrollManagement.Application.SalaryStructure.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.SalaryStructure.Queries
{
    public static class Queries
    {
        public class GetSalaryStructureList : IRequest<List<SalaryStructureVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetSalaryStructure : IRequest<SalaryStructureVM>
        {
            public Guid Id { get; set; }
        }
    }
}
