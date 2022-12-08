using PayrollManagement.Application.EmployeeBankAccount.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeBankAccount.Queries
{
    public class GetEmployeeBankAccountQueryHandler : IRequestHandler<Queries.GetEmployeeBankAccount, EmployeeBankAccountVM>
    {
        public GetEmployeeBankAccountQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeBankAccountVM>
            Handle(Queries.GetEmployeeBankAccount request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeBankAccountById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeBankAccountVM>
                (query);
            return data;
        }
    }
}

