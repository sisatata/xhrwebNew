using PayrollManagement.Application.BonusConfiguration.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BonusConfiguration.Queries
{
    public class GetBonusConfigurationListQueryHandler : IRequestHandler<Queries.GetBonusConfigurationList, List<BonusConfigurationVM>>
    {
        public GetBonusConfigurationListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BonusConfigurationVM>> Handle(Queries.GetBonusConfigurationList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBonusConfigurationListByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<BonusConfigurationVM>(query);
            return data.ToList();
        }
    }
}
