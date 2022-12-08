using EmployeeEnrollment.Application.EmployeeManager.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeManager.Queries
{
    public class GetEmployeeManagerListQueryHandler : IRequestHandler<Queries.GetEmployeeManagerList, List<EmployeeManagerVM>>
    {
        public GetEmployeeManagerListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeManagerVM>> Handle(Queries.GetEmployeeManagerList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeManagerByEmployee('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeManagerVM>(query);
            return data.ToList();
        }
    }
}
