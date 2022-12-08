using CompanySetup.Application.SalaryRuleDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.SalaryRuleDetail.Queries
{
    public class GetSalaryRuleDetailQueryHandler : IRequestHandler<Queries.GetSalaryRuleDetail, SalaryRuleDetailVM>
    {
        public GetSalaryRuleDetailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<SalaryRuleDetailVM>
            Handle(Queries.GetSalaryRuleDetail request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetSalaryRuleDetailById ({request.Id})";

            var data = await _connection.QueryFirstAsync<SalaryRuleDetailVM>
                (query);
            return data;
        }
    }
}

