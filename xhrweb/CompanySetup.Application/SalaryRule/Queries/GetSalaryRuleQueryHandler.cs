using CompanySetup.Application.SalaryRule.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.SalaryRule.Queries
{
    public class GetSalaryRuleQueryHandler : IRequestHandler<Queries.GetSalaryRule, SalaryRuleVM>
    {
        public GetSalaryRuleQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<SalaryRuleVM>
            Handle(Queries.GetSalaryRule request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetSalaryRuleById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<SalaryRuleVM>
                (query);
            return data;
        }
    }
}

