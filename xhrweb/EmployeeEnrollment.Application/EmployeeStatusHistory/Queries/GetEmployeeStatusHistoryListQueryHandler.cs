using EmployeeEnrollment.Application.EmployeeStatusHistory.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Queries
{
    public class GetEmployeeStatusHistoryListQueryHandler : IRequestHandler<Queries.GetEmployeeStatusHistoryList, List<EmployeeStatusHistoryVM>>
    {
        public GetEmployeeStatusHistoryListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeStatusHistoryVM>> Handle(Queries.GetEmployeeStatusHistoryList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeStatusHistoryByEmployee ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeStatusHistoryVM>(query);
            return data.ToList();
        }
    }
}
