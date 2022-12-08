using PayrollManagement.Application.IncomeTaxPayerType.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.IncomeTaxPayerType.Queries
{
    public class GetIncomeTaxPayerTypeQueryHandler : IRequestHandler<Queries.GetIncomeTaxPayerType, IncomeTaxPayerTypeVM>
    {
        public GetIncomeTaxPayerTypeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<IncomeTaxPayerTypeVM>
            Handle(Queries.GetIncomeTaxPayerType request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetIncomeTaxPayerTypeById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<IncomeTaxPayerTypeVM>
                (query);
            return data;
        }
    }
}

