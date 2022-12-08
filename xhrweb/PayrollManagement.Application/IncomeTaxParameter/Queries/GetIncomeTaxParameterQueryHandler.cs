using PayrollManagement.Application.IncomeTaxParameter.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.IncomeTaxParameter.Queries
{
    public class GetIncomeTaxParameterQueryHandler : IRequestHandler<Queries.GetIncomeTaxParameter, IncomeTaxParameterVM>
    {
        public GetIncomeTaxParameterQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<IncomeTaxParameterVM>
            Handle(Queries.GetIncomeTaxParameter request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetIncomeTaxParameterById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<IncomeTaxParameterVM>
                (query);
            return data;
        }
    }
}

