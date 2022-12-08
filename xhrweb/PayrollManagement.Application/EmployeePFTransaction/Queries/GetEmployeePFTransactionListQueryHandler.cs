using PayrollManagement.Application.EmployeePFTransaction.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeePFTransaction.Queries
{
    public class GetEmployeePFTransactionListQueryHandler : IRequestHandler<Queries.GetEmployeePFTransactionList, List<EmployeePFTransactionVM>>
    {
        public GetEmployeePFTransactionListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeePFTransactionVM>> Handle(Queries.GetEmployeePFTransactionList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetEmployeePFTransactionById ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<EmployeePFTransactionVM>(query);
            return data.ToList();
        }
    }
}
