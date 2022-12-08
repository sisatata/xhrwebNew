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
    public class GetSalaryRuleListQueryHandler : IRequestHandler<Queries.GetSalaryRuleList, List<SalaryRuleVM>>
    {
        public GetSalaryRuleListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<SalaryRuleVM>> Handle(Queries.GetSalaryRuleList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetSalaryRuleByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<SalaryRuleVM>(query);
            return data.ToList();
        }
    }
}
