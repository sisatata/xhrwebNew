using EmployeeEnrollment.Application.EmployeeStatus.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeStatus.Queries
{
    public class GetEmployeeStatusListQueryHandler : IRequestHandler<Queries.GetEmployeeStatusList, List<EmployeeStatusVM>>
    {
        public GetEmployeeStatusListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeStatusVM>> Handle(Queries.GetEmployeeStatusList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeStatusByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<EmployeeStatusVM>(query);
            return data.ToList();
        }
    }
}
