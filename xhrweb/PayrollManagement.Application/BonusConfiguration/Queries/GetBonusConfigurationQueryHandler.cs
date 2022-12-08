using PayrollManagement.Application.BonusConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BonusConfiguration.Queries
{
    public class GetBonusConfigurationQueryHandler : IRequestHandler<Queries.GetBonusConfiguration, BonusConfigurationVM>
    {
        public GetBonusConfigurationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BonusConfigurationVM>
            Handle(Queries.GetBonusConfiguration request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBonusConfigurationById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<BonusConfigurationVM>
                (query);
            return data;
        }
    }
}

