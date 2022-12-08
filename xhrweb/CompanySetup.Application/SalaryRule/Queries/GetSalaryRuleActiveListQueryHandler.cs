using CompanySetup.Application.SalaryRule.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.SalaryRule.Queries
{
    public class GetSalaryRuleActiveListQueryHandler : IRequestHandler<Queries.GetSalaryRuleActiveList, List<SalaryRuleVM>>
    {
        public GetSalaryRuleActiveListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<SalaryRuleVM>> Handle(Queries.GetSalaryRuleActiveList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetSalaryRuleActiveByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<SalaryRuleVM>(query);
            return data.ToList();
        }
    }
}
