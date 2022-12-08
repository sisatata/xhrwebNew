using PayrollManagement.Application.EmployeePFTransaction.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeePFTransaction.Queries
{
    public class GetEmployeePFTransactionQueryHandler : IRequestHandler<Queries.GetEmployeePFTransaction, EmployeePFTransactionVM>
    {
        public GetEmployeePFTransactionQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeePFTransactionVM>
            Handle(Queries.GetEmployeePFTransaction request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetEmployeePFTransactionById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeePFTransactionVM> (query);
            return data;
        }
    }
}

