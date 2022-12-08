using EmployeeEnrollment.Application.EmployeeEmail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeEmail.Queries
{
    public class GetEmployeeEmailListQueryHandler : IRequestHandler<Queries.GetEmployeeEmailListByEmployeeId, List<EmployeeEmailVM>>
    {
        public GetEmployeeEmailListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeEmailVM>> Handle(Queries.GetEmployeeEmailListByEmployeeId request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeEmailListByEmployeeId ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeEmailVM>(query);
            return data.ToList();
        }
    }
}
