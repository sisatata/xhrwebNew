using PayrollManagement.Application.SalaryStructureComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.SalaryStructureComponent.Queries
{
    public static class Queries
    {
        public class GetSalaryStructureComponentList : IRequest<List<SalaryStructureComponentVM>>
        {
            public Guid StructureId  { get; set; }
        }

        public class GetSalaryStructureComponent : IRequest<SalaryStructureComponentVM>
        {
            public Guid Id { get; set; }
        }
    }
}
