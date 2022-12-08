using PayrollManagement.Application.EmployeeBonusProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Queries
{
    public static class Queries
    {
        public class GetEmployeeBonusProcessedDataList : IRequest<List<EmployeeBonusProcessedDataVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid BonusYearId { get; set; }
        }

        public class GetEmployeeBonusProcessedData : IRequest<EmployeeBonusProcessedDataVM>
        {
            public Guid Id { get; set; }
        }
    }
}
