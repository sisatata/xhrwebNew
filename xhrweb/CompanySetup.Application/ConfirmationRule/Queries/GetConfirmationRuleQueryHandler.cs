using CompanySetup.Application.ConfirmationRule.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.ConfirmationRule.Queries
{
    public class GetConfirmationRuleQueryHandler : IRequestHandler<Queries.GetConfirmationRule, ConfirmationRuleVM>
    {
        public GetConfirmationRuleQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<ConfirmationRuleVM>
            Handle(Queries.GetConfirmationRule request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetConfirmationRuleById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<ConfirmationRuleVM>
                (query);
            return data;
        }
    }
}

