using PayrollManagement.Application.IncomeTaxSlab.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.IncomeTaxSlab.Queries
{
    public class GetIncomeTaxSlabQueryHandler : IRequestHandler<Queries.GetIncomeTaxSlab, IncomeTaxSlabVM>
    {
        public GetIncomeTaxSlabQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<IncomeTaxSlabVM>
            Handle(Queries.GetIncomeTaxSlab request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetIncomeTaxSlabById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<IncomeTaxSlabVM>
                (query);
            return data;
        }
    }
}

