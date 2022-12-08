using PayrollManagement.Application.IncomeTaxInvestment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.IncomeTaxInvestment.Queries
{
    public class GetIncomeTaxInvestmentQueryHandler : IRequestHandler<Queries.GetIncomeTaxInvestment, IncomeTaxInvestmentVM>
    {
        public GetIncomeTaxInvestmentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<IncomeTaxInvestmentVM>
            Handle(Queries.GetIncomeTaxInvestment request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetIncomeTaxInvestmentById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<IncomeTaxInvestmentVM>
                (query);
            return data;
        }
    }
}

