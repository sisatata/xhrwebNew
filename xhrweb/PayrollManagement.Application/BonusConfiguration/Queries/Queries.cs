using PayrollManagement.Application.BonusConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BonusConfiguration.Queries
{
    public static class Queries
    {
        public class GetBonusConfigurationList : IRequest<List<BonusConfigurationVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBonusConfiguration : IRequest<BonusConfigurationVM>
        {
            public Guid Id { get; set; }
        }
    }
}
