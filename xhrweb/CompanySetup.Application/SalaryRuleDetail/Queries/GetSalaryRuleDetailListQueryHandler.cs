using CompanySetup.Application.SalaryRuleDetail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.SalaryRuleDetail.Queries
{
    public class GetSalaryRuleDetailListQueryHandler : IRequestHandler<Queries.GetSalaryRuleDetailList, List<SalaryRuleDetailVM>>
    {
        public GetSalaryRuleDetailListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<SalaryRuleDetailVM>> Handle(Queries.GetSalaryRuleDetailList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetSalaryRuleDetailById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<SalaryRuleDetailVM>(query);
            return data.ToList();
        }
    }
}
