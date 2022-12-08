using PayrollManagement.Application.Bank.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.Bank.Queries
{
    public class GetBankQueryHandler : IRequestHandler<Queries.GetBank, BankVM>
    {
        public GetBankQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BankVM>
            Handle(Queries.GetBank request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBankById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<BankVM>
                (query);
            return data;
        }
    }
}

