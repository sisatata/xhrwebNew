using CompanySetup.Application.ConfirmationRule.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.ConfirmationRule.Queries
{
    public class GetConfirmationRuleActiveListQueryHandler : IRequestHandler<Queries.GetConfirmationRuleActiveList, List<ConfirmationRuleVM>>
    {
        public GetConfirmationRuleActiveListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ConfirmationRuleVM>> Handle(Queries.GetConfirmationRuleActiveList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetConfirmationRuleActiveByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<ConfirmationRuleVM>(query);
            return data.ToList();
        }
    }
}
